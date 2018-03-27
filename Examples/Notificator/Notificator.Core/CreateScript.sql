--USE [Notificator4]
GO
/****** Object:  Table [dbo].[Events]    Script Date: 02/20/2011 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Applications]    Script Date: 02/20/2011 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Applications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Applications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[GetEventById]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetEventById] 
    @Id INT
AS 
	SELECT [Id], [Name] 
	FROM   [dbo].[Events] 
	WHERE  ([Id] = @Id OR @Id IS NULL)
GO
/****** Object:  StoredProcedure [dbo].[GetApplicationByName]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetApplicationByName] 
    @Name NVARCHAR(100)
AS 
	SELECT [Id], [Name] 
	FROM   [dbo].[Applications] 
	WHERE  ([Name] = @Name)
GO
/****** Object:  StoredProcedure [dbo].[GetApplicationById]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetApplicationById] 
    @Id INT
AS 
	SELECT [Id], [Name] 
	FROM   [dbo].[Applications] 
	WHERE [Id] = @Id
GO
/****** Object:  StoredProcedure [dbo].[usp_EventsUpdate]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_EventsUpdate] 
    @Id int,
    @Name nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Events]
	SET    [Name] = @Name
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name]
	FROM   [dbo].[Events]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_EventsInsert]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_EventsInsert] 
    @Name nvarchar(MAX)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Events] ([Name])
	SELECT @Name
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name]
	FROM   [dbo].[Events]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_EventsDelete]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_EventsDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Events]
	WHERE  [Id] = @Id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_ApplicationsUpdate]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_ApplicationsUpdate] 
    @Id int,
    @Name nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Applications]
	SET    [Name] = @Name
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name]
	FROM   [dbo].[Applications]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_ApplicationsInsert]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_ApplicationsInsert] 
    @Name nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Applications] ([Name])
	SELECT @Name
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Name]
	FROM   [dbo].[Applications]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_ApplicationsDelete]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_ApplicationsDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Applications]
	WHERE  [Id] = @Id

	COMMIT
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02/20/2011 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[IsAnonymous] [bit] NOT NULL,
	[LastActivityDate] [datetime] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordFormat] [int] NOT NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordQuestion] [nvarchar](128) NULL,
	[PasswordAnswer] [nvarchar](128) NULL,
	[IsApproved] [bit] NOT NULL,
	[CreateOn] [datetime] NOT NULL,
	[LastLoginDate] [datetime] NOT NULL,
	[LastPasswordChangedDate] [datetime] NOT NULL,
	[LastLockoutDate] [datetime] NOT NULL,
	[FailedPasswordAttemptCount] [int] NOT NULL,
	[FailedPasswordAttemptWindowStart] [datetime] NOT NULL,
	[FailedPasswordAnswerAttemptCount] [int] NOT NULL,
	[FailedPasswordAnswerAttemptWindowStart] [datetime] NOT NULL,
	[Comment] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[TimeZone] [int] NULL,
	[Status] [tinyint] NOT NULL,
	[ApplicationId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_Users_Applications] ON [dbo].[Users] 
(
	[ApplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEventLinks]    Script Date: 02/20/2011 23:07:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEventLinks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserEventLinks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [CK_Unique_UserEvent] UNIQUE NONCLUSTERED 
(
	[UserId] ASC,
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UserEventLink_Events] ON [dbo].[UserEventLinks] 
(
	[EventId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_FK_UserEventLink_Users] ON [dbo].[UserEventLinks] 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_UsersUpdate]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UsersUpdate] 
    @Id int,
    @Username nvarchar(256),
    @Email nvarchar(256),
    @IsAnonymous bit,
    @LastActivityDate datetime,
    @Password nvarchar(128),
    @PasswordFormat int,
    @PasswordSalt nvarchar(128),
    @PasswordQuestion nvarchar(128),
    @PasswordAnswer nvarchar(128),
    @IsApproved bit,
    @CreateOn datetime,
    @LastLoginDate datetime,
    @LastPasswordChangedDate datetime,
    @LastLockoutDate datetime,
    @FailedPasswordAttemptCount int,
    @FailedPasswordAttemptWindowStart datetime,
    @FailedPasswordAnswerAttemptCount int,
    @FailedPasswordAnswerAttemptWindowStart datetime,
    @Comment nvarchar(MAX),
    @FirstName nvarchar(100),
    @LastName nvarchar(100),
    @TimeZone int,
    @Status tinyint,
    @ApplicationId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Users]
	SET    [Username] = @Username, [Email] = @Email, [IsAnonymous] = @IsAnonymous, [LastActivityDate] = @LastActivityDate, [Password] = @Password, [PasswordFormat] = @PasswordFormat, [PasswordSalt] = @PasswordSalt, [PasswordQuestion] = @PasswordQuestion, [PasswordAnswer] = @PasswordAnswer, [IsApproved] = @IsApproved, [CreateOn] = @CreateOn, [LastLoginDate] = @LastLoginDate, [LastPasswordChangedDate] = @LastPasswordChangedDate, [LastLockoutDate] = @LastLockoutDate, [FailedPasswordAttemptCount] = @FailedPasswordAttemptCount, [FailedPasswordAttemptWindowStart] = @FailedPasswordAttemptWindowStart, [FailedPasswordAnswerAttemptCount] = @FailedPasswordAnswerAttemptCount, [FailedPasswordAnswerAttemptWindowStart] = @FailedPasswordAnswerAttemptWindowStart, [Comment] = @Comment, [FirstName] = @FirstName, [LastName] = @LastName, [TimeZone] = @TimeZone, [Status] = @Status, [ApplicationId] = @ApplicationId
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Username], [Email], [IsAnonymous], [LastActivityDate], [Password], [PasswordFormat], [PasswordSalt], [PasswordQuestion], [PasswordAnswer], [IsApproved], [CreateOn], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment], [FirstName], [LastName], [TimeZone], [Status], [ApplicationId]
	FROM   [dbo].[Users]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_UsersInsert]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UsersInsert] 
    @Username nvarchar(256),
    @Email nvarchar(256),
    @IsAnonymous bit,
    @LastActivityDate datetime,
    @Password nvarchar(128),
    @PasswordFormat int,
    @PasswordSalt nvarchar(128),
    @PasswordQuestion nvarchar(128),
    @PasswordAnswer nvarchar(128),
    @IsApproved bit,
    @CreateOn datetime,
    @LastLoginDate datetime,
    @LastPasswordChangedDate datetime,
    @LastLockoutDate datetime,
    @FailedPasswordAttemptCount int,
    @FailedPasswordAttemptWindowStart datetime,
    @FailedPasswordAnswerAttemptCount int,
    @FailedPasswordAnswerAttemptWindowStart datetime,
    @Comment nvarchar(MAX),
    @FirstName nvarchar(100),
    @LastName nvarchar(100),
    @TimeZone int,
    @Status tinyint,
    @ApplicationId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Users] ([Username], [Email], [IsAnonymous], [LastActivityDate], [Password], [PasswordFormat], [PasswordSalt], [PasswordQuestion], [PasswordAnswer], [IsApproved], [CreateOn], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment], [FirstName], [LastName], [TimeZone], [Status], [ApplicationId])
	SELECT @Username, @Email, @IsAnonymous, @LastActivityDate, @Password, @PasswordFormat, @PasswordSalt, @PasswordQuestion, @PasswordAnswer, @IsApproved, @CreateOn, @LastLoginDate, @LastPasswordChangedDate, @LastLockoutDate, @FailedPasswordAttemptCount, @FailedPasswordAttemptWindowStart, @FailedPasswordAnswerAttemptCount, @FailedPasswordAnswerAttemptWindowStart, @Comment, @FirstName, @LastName, @TimeZone, @Status, @ApplicationId
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [Username], [Email], [IsAnonymous], [LastActivityDate], [Password], [PasswordFormat], [PasswordSalt], [PasswordQuestion], [PasswordAnswer], [IsApproved], [CreateOn], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment], [FirstName], [LastName], [TimeZone], [Status], [ApplicationId]
	FROM   [dbo].[Users]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_UsersDelete]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UsersDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Users]
	WHERE  [Id] = @Id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GetUserByName]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserByName] 
    @Name NVARCHAR(256),
    @ApplicationId INT
AS 
	SELECT [Id], [Username], [Email], [IsAnonymous], [LastActivityDate], [Password], [PasswordFormat], [PasswordSalt], [PasswordQuestion], [PasswordAnswer], [IsApproved], [CreateOn], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment], [FirstName], [LastName], [TimeZone], [Status], [ApplicationId] 
	FROM   [dbo].[Users] 
	WHERE  ([ApplicationId] = @ApplicationId AND [Username] = @Name)
GO
/****** Object:  StoredProcedure [dbo].[GetUserById]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserById] 
    @Id INT,
    @ApplicationId INT
AS 
	SELECT [Id], [Username], [Email], [IsAnonymous], [LastActivityDate], [Password], [PasswordFormat], [PasswordSalt], [PasswordQuestion], [PasswordAnswer], [IsApproved], [CreateOn], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment], [FirstName], [LastName], [TimeZone], [Status], [ApplicationId] 
	FROM   [dbo].[Users] 
	WHERE  [Id] = @Id AND ApplicationId = @ApplicationId
GO
/****** Object:  StoredProcedure [dbo].[GetNotificationLog]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetNotificationLog] 
    @UserId INT,
    @LastLinkId INT,
    @TopCount INT
AS 

SELECT TOP(@TopCount) u.Id as UserId, u.UserName as UserName, links.Id as LinkId, links.CreatedDate as JoinDate, e.Name as EventName, e.Id as EventId
FROM 
UserEventLinks links JOIN
	(
		SELECT EventId FROM UserEventLinks WHERE UserId = @UserId
	) As UserEvents 
ON links.EventId = UserEvents.EventId 
	AND links.UserId <> @UserId 
	AND links.Id > @LastLinkId
JOIN [Events] e ON links.EventId = e.Id
JOIN Users u ON u.Id = links.UserId
ORDER BY JoinDate DESC
GO
/****** Object:  StoredProcedure [dbo].[usp_UserEventLinksUpdate]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UserEventLinksUpdate] 
    @Id int,
    @UserId int,
    @EventId int,
    @CreatedDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[UserEventLinks]
	SET    [UserId] = @UserId, [EventId] = @EventId, [CreatedDate] = @CreatedDate
	WHERE  [Id] = @Id
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [UserId], [EventId], [CreatedDate]
	FROM   [dbo].[UserEventLinks]
	WHERE  [Id] = @Id	
	-- End Return Select <- do not remove

	COMMIT TRAN
GO
/****** Object:  StoredProcedure [dbo].[usp_UserEventLinksInsert]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UserEventLinksInsert] 
    @UserId int,
    @EventId int,
    @CreatedDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[UserEventLinks] ([UserId], [EventId], [CreatedDate])
	SELECT @UserId, @EventId, @CreatedDate
	
	-- Begin Return Select <- do not remove
	SELECT [Id], [UserId], [EventId], [CreatedDate]
	FROM   [dbo].[UserEventLinks]
	WHERE  [Id] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[usp_UserEventLinksDelete]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_UserEventLinksDelete] 
    @Id int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[UserEventLinks]
	WHERE  [Id] = @Id

	COMMIT
GO
/****** Object:  StoredProcedure [dbo].[GetAllEvents]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetAllEvents] 
    @UserId INT
AS 
	SELECT e.Id, e.Name, COALESCE(Links.UserCount, NULL, 0) as UserCount, link.CreatedDate as JoinDate 
	FROM 
		Events e LEFT JOIN
		(
			SELECT EventId, COUNT(UserId) as UserCount 
			FROM UserEventLinks 
			GROUP BY EventId
		) as Links
	 ON Links.EventId = Id
	 LEFT JOIN UserEventLinks link ON e.Id = link.EventId AND link.UserId = @UserId
	 ORDER BY e.Id DESC
GO
/****** Object:  StoredProcedure [dbo].[GetUserEvents]    Script Date: 02/20/2011 23:07:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[GetUserEvents] 
    @UserId INT
AS 
	SELECT e.Id, e.Name, COALESCE(Links.UserCount, NULL, 0) as UserCount, link.CreatedDate as JoinDate 
	FROM 
		Events e LEFT JOIN
		(
			SELECT EventId, COUNT(UserId) as UserCount 
			FROM UserEventLinks 
			GROUP BY EventId
		) as Links
	 ON Links.EventId = Id
	 JOIN UserEventLinks link ON e.Id = link.EventId AND link.UserId = @UserId
	 ORDER BY link.CreatedDate DESC
GO
/****** Object:  ForeignKey [FK_UserEventLink_Events]    Script Date: 02/20/2011 23:07:23 ******/
ALTER TABLE [dbo].[UserEventLinks]  WITH CHECK ADD  CONSTRAINT [FK_UserEventLink_Events] FOREIGN KEY([EventId])
REFERENCES [dbo].[Events] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserEventLinks] CHECK CONSTRAINT [FK_UserEventLink_Events]
GO
/****** Object:  ForeignKey [FK_UserEventLink_Users]    Script Date: 02/20/2011 23:07:23 ******/
ALTER TABLE [dbo].[UserEventLinks]  WITH CHECK ADD  CONSTRAINT [FK_UserEventLink_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserEventLinks] CHECK CONSTRAINT [FK_UserEventLink_Users]
GO
/****** Object:  ForeignKey [FK_Users_Applications]    Script Date: 02/20/2011 23:07:24 ******/
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Applications] FOREIGN KEY([ApplicationId])
REFERENCES [dbo].[Applications] ([Id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Applications]
GO
