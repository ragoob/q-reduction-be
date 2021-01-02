using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class GetBranchOpenShiftIdsStoredProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql(@"Create Procedure GetBranchOpenShiftIds ( @BranchId int,@CurrentTime time)
                        as
                        begin

                        ; with _shift as
                        (
                            select*,
                                shift_start = convert(datetime, Shifts.[Start]),--[Start]
                                shift_end = case  when Shifts.[Start] < Shifts.[End]
                                            then convert(datetime, Shifts.[Start])
                                            else dateadd(day, 1, convert(datetime, Shifts.[End]))
                                            end
                            from    Shifts
                        )
                        select Id,

                                [Code],

                                [StartAt],

                                [EndAt],

                                [BranchId],

                                [IsEnded],

                                [QRCode],

                                [UserIdSupport],

                                [UpdateAt],

                                [CreateBy],

                                [CreateAt],

                                [UpdateBy],

                                [End],

                                [Start]

                        from _shift
                        where BranchId = @BranchId And
                        (convert(datetime, @Currenttime) between shift_start and shift_end
                        or

                        dateadd(day, 1, convert(datetime, @Currenttime)) between shift_start and shift_end)

                        End");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
