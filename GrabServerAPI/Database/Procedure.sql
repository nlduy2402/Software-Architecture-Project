GO
USE GrabSystemDB

CREATE OR ALTER PROCEDURE USP_GetNextColumnId(
	@tablename SYSNAME,
	@columnname SYSNAME
)
AS
	DECLARE @IntVariable INT = 0;  
	DECLARE @SQLString NVARCHAR(MAX);  
	DECLARE @ParmDefinition NVARCHAR(500);
  
	SET @SQLString = 
		N'with cte as (select ' + @columnname + ' id, lead(' + @columnname + ') over (order by ' + @columnname + ') nextid from ' + @tablename + ')
		select @gapstartOUT = MIN(id) from cte
		where id < nextid - 1';
	SET @ParmDefinition = N'@gapstartOUT INTEGER OUTPUT';  
  
	EXECUTE sp_executesql @SQLString, @ParmDefinition, @gapstartOUT = @IntVariable OUTPUT;  

	IF (@IntVariable IS NULL)
	BEGIN
		DECLARE @SQL NVARCHAR(MAX);
		SET @SQL = N'select @IdOUT = count(' + @columnname + ') from ' + @tablename + '';
		SET @ParmDefinition = N'@IdOUT INTEGER OUTPUT';
		EXEC sp_executesql @SQL, @ParmDefinition, @IdOUT = @IntVariable OUTPUT;

		RETURN @IntVariable + 1;
	END

	RETURN @IntVariable + 1; 

---------------------------- CRUD ACCOUNT ----------------------------
-- CREATE

CREATE OR ALTER PROCEDURE USP_AddAccount

    @Username VARCHAR(50),
    @PasswordHash VARCHAR(500),
    @PasswordSalt VARCHAR(500),
    @UserRole VARCHAR(50),
    @RefreshToken VARCHAR(200),
    @TokenCreated DATETIME,
    @TokenExpires DATETIME
AS
	BEGIN TRY
		DECLARE @Id INT
		EXEC @Id = dbo.USP_GetNextColumnId 'ACCOUNT', 'Id'

		INSERT INTO ACCOUNT VALUES (@Id, 0.0, 0.0, @Username, @PasswordHash, @PasswordSalt, @UserRole, @RefreshToken, @TokenCreated, @TokenExpires)
		RETURN @Id
	END TRY

	BEGIN CATCH
		PRINT N'Account insertion error'
		RETURN -1
	END CATCH
GO
-- READ
CREATE OR ALTER PROC USP_GetAllAccount
AS
	SELECT * FROM ACCOUNT
GO


CREATE OR ALTER PROC USP_GetAccountByUsername
	@Username VARCHAR(50)
AS
	SELECT * FROM ACCOUNT where Username like @Username
GO


CREATE OR ALTER PROC USP_GetAccountById
	@Id INT
AS
	IF (@Id = -1)
	BEGIN
		SELECT null;
	END

	SELECT * FROM ACCOUNT where Id = @Id;
GO
-- UPDATE
GO
CREATE OR ALTER PROC USP_UpdateAccount
	@Id INTEGER,
	@Username VARCHAR(50),
	@PasswordHash VARCHAR(500),
	@PasswordSalt VARCHAR(500)
	--@UserRole VARCHAR(50),
 --   @RefreshToken VARCHAR(200),
 --   @TokenCreated DATETIME,
 --   @TokenExpires DATETIME
AS
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM ACCOUNT WHERE Id = @Id)
		BEGIN
			PRINT N'Account Id not existed'
			RETURN 1
		END

		UPDATE ACCOUNT 
		SET
			Username = @Username,
			PasswordHash = @PasswordHash,
			PasswordSalt = @PasswordSalt
		WHERE Id = @Id
		RETURN 0
	END TRY

	BEGIN CATCH
		PRINT N'Account update error'
		RETURN 1
	END CATCH
GO

GO
CREATE OR ALTER PROC USP_UpdatePositionAccount
	@Id INTEGER,
	@Long FLOAT,
    @Lat FLOAT
	--@UserRole VARCHAR(50),
 --   @RefreshToken VARCHAR(200),
 --   @TokenCreated DATETIME,
 --   @TokenExpires DATETIME
AS
	BEGIN TRY
		IF NOT EXISTS (SELECT * FROM ACCOUNT WHERE Id = @Id)
		BEGIN
			PRINT N'Account Id not existed'
			RETURN 1
		END

		UPDATE ACCOUNT 
		SET
			Long = @Long,
			Lat = @Lat
		WHERE Id = @Id
		RETURN 0
	END TRY

	BEGIN CATCH
		PRINT N'Account update error'
		RETURN 1
	END CATCH
GO
-- DELETE
GO
CREATE OR ALTER PROC USP_DeleteAccount
	@Id INTEGER
AS
	BEGIN TRY
		

		DELETE FROM ACCOUNT WHERE Id = @Id
		RETURN 0
	END TRY

	BEGIN CATCH
		PRINT N'Account deletion error'
		RETURN 1
	END CATCH
GO

