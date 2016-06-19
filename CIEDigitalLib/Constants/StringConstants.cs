namespace CIEDigitalLib.Constants
{
    public class StringConstants
    {
        #region Create Elmah Tables

        public const string Create_Elmah_Tables = @"CREATE TABLE ELMAH_Error
(
    ErrorId     UNIQUEIDENTIFIER	DEFAULT(NEWID()) NOT NULL,
    [Application] NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Host        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Type]        NVARCHAR(100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    Source      NVARCHAR(60)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [Message]     NVARCHAR(500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    [User]        NVARCHAR(50)  COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
    StatusCode  INT NOT NULL,
    TimeUtc     DATETIME NOT NULL,
    Sequence    INT IDENTITY (1, 1) NOT NULL,
    AllXml      NTEXT COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	CONSTRAINT PK_ELMAH_Error PRIMARY KEY NONCLUSTERED (ErrorId)
)

CREATE NONCLUSTERED INDEX IX_ELMAH_Error_App_Time_Seq ON ELMAH_Error (Application   ASC, TimeUtc DESC, Sequence DESC)
";

        #endregion Create Elmah Tables

        #region Create_Elmah_LogError_Proc

        public const string Create_Elmah_LogError_Proc = @"CREATE PROCEDURE ELMAH_LogError
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NTEXT,
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        ELMAH_Error
        (
            ErrorId,
            [Application],
            Host,
            [Type],
            Source,
            [Message],
            [User],
            AllXml,
            StatusCode,
            TimeUtc
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )";

        #endregion Create_Elmah_LogError_Proc

        #region Create_Elmah_GetErrorXml_Proc

        public const string Create_Elmah_GetErrorXml_Proc = @"CREATE PROCEDURE ELMAH_GetErrorXml
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        AllXml
    FROM 
        ELMAH_Error
    WHERE
        ErrorId = @ErrorId
    AND
        Application = @Application

";

        #endregion Create_Elmah_GetErrorXml_Proc

        #region Create_Elmah_GetErrorsXml_Proc

        public const string Create_Elmah_GetErrorsXml_Proc = @"CREATE PROCEDURE ELMAH_GetErrorsXml
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        ELMAH_Error
    WHERE 
        Application = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = TimeUtc,
            @FirstSequence = Sequence
        FROM 
            ELMAH_Error
        WHERE   
            Application = @Application
        ORDER BY 
            TimeUtc DESC, 
            Sequence DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = ErrorId, 
        [application] = [Application],
        host        = Host, 
        [type]        = [Type],
        source      = Source,
        [message]     = [Message],
        [user]      = [User],
        statusCode  = StatusCode, 
        time        = CONVERT(VARCHAR(50), TimeUtc, 126) + 'Z'
    FROM 
        ELMAH_Error error
    WHERE
        Application = @Application
    AND
        TimeUtc <= @FirstTimeUTC
    AND 
        Sequence <= @FirstSequence
    ORDER BY
        TimeUtc DESC, 
        Sequence DESC
    FOR
        XML AUTO";

        #endregion Create_Elmah_GetErrorsXml_Proc
    }
}