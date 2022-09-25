using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api2.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderEventID = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Odds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderOddsID = table.Column<int>(type: "int", nullable: false),
                    ProviderEventID = table.Column<int>(type: "int", nullable: false),
                    OddsName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OddsRate = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odds", x => x.Id);
                });
            var sp = @"



-- =================================================================
-- Author:		DUDKO MYKOLA
-- Create date: 20.08.2022
-- Description:	INSERT/UPDATE TABLE EVENT/ODDS
-- =================================================================
CREATE procedure [dbo].[sp_event_odd_do]
(
	@s_items					nvarchar(max)
)
as

    declare @p_roviderEventID INT;
	set @p_roviderEventID =(SELECT TOP 1 [ProviderEventID] from openjson(@s_items, '$') WITH ([ProviderEventID] INT '$.ProviderEventID'));

	
    /*UPDATE*/

  if (select count(*) from [dbo].[Event] where [ProviderEventID] = @p_roviderEventID) > 0
  begin

	UPDATE [dbo].[Event] 
	set [EventDate]  = J.[EventDate]
    from openjson(@s_items) 	
    WITH (
	[ProviderEventID] INT '$.ProviderEventID', 
    [EventDate] DATETIME '$.EventDate'
	) J where  [dbo].[Event] .[ProviderEventID] = @p_roviderEventID; 


	UPDATE [dbo].Odds 
	set [OddsRate] = J.[OddsRate],
	    [Status] = J.[Status]
    from openjson(@s_items, '$.OddsList') 
    WITH (
	[ProviderOddsID] INT '$.ProviderOddsID', 
	[OddsRate] FLOAT '$.OddsRate',
	[Status] NVARCHAR(50) '$.Status'	
	)J where  [dbo].Odds.[ProviderOddsID] = J.[ProviderOddsID]; 
   end

else
  
  	/*INSERT*/

  begin	
    INSERT INTO [dbo].[Event] 
	SELECT [ProviderEventID],[EventName],[EventDate]
    from openjson(@s_items, '$') 
    WITH (
	[ProviderEventID] INT '$.ProviderEventID', 
	[EventName] NVARCHAR(50) '$.EventName',
    [EventDate] DATETIME '$.EventDate'
	);

	select @p_roviderEventID;
 
	INSERT INTO [dbo].Odds 
    SELECT [ProviderOddsID],@p_roviderEventID as [ProviderEventID] ,[OddsName],[OddsRate],[Status]
    from openjson(@s_items, '$.OddsList') 
    WITH (
	[ProviderOddsID] INT '$.ProviderOddsID', 
	[OddsName] NVARCHAR(50) '$.OddsName',
	[OddsRate] FLOAT '$.OddsRate',
	[Status] NVARCHAR(50) '$.Status'	
	);
  end
";
            migrationBuilder.Sql(sp);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.DropTable(
                name: "Odds");
        }
    }
}
