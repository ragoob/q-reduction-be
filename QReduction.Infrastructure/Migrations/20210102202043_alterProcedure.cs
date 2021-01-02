﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class alterProcedure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Alter Procedure GetBranchOpenShiftIds ( @BranchId int,@CurrentTime time)
                        as
                        begin

                        ; with _shift as
                        (
                            select*,
                                shift_start = convert(datetime, Shifts.[Start]),--[Start]
                                shift_end = case  when Shifts.[Start] < Shifts.[End]
                                            then convert(datetime, Shifts.[End])
                                            else dateadd(day, 1, convert(datetime, Shifts.[End]))
                                            end
                            from    Shifts
                            WHERE BranchId = @BranchId
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
                        where 
                        convert(datetime, @Currenttime) between shift_start and shift_end
                        or

                        dateadd(day, 1, convert(datetime, @Currenttime)) between shift_start and shift_end

                        End");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
