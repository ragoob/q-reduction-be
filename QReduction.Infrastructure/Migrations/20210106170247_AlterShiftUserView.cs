using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class AlterShiftUserView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"				ALTER PROCEDURE [dbo].[ShiftUserPerDay](@UserId int, @BranchId int,@CurrentTime time)
				as
				BEGIN
				 with shiftUserCte as
								(
									SELECT
										[dbo].[ShiftUsers].[Id],
										[ShiftId],
										[WindowNumber],
										[ServiceId],
										[dbo].[Shifts].[Start],
										[dbo].[Shifts].[End] ,
										shift_start = convert(datetime, Shifts.[Start]),
										shift_end = case  when Shifts.[Start] < Shifts.[End]
													then convert(datetime, Shifts.[End])
													else dateadd(day, 1, convert(datetime, Shifts.[End]))
													end

									FROM[dbo].[ShiftUsers]  INNER JOIN[dbo].[Shifts]
			ON[dbo].[ShiftUsers].[ShiftId] = [dbo].[Shifts].[Id]

									WHERE[dbo].[ShiftUsers].[UserId] = @UserId AND
										   [dbo].[Shifts].BranchId = @BranchId--AND
										  --CONVERT(nvarchar, CreateAt, 23) = CONVERT(nvarchar, GETDATE(), 23)
								
								)
						SELECT
							[Id],
							[ShiftId],
							[WindowNumber],
							[ServiceId],
							[Start],
							[End]
			FROM shiftUserCte
						where
							convert(datetime, @Currenttime) between shift_start and shift_end
							or dateadd(day, 1, convert(datetime, @Currenttime)) between shift_start and shift_end



				END" );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
