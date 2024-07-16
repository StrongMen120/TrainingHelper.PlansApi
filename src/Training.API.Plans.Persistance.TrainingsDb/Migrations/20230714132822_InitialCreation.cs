using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Training.API.Plans.Persistance.TrainingsDb.Entities;

#nullable disable

namespace Training.API.Plans.Persistance.TrainingsDb.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "trainings");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:trainings.PlansImage", "monday,tuesday,wednesday,thursday,friday,saturday,sunday,push,pull,legs,upper,lower")
                .Annotation("Npgsql:Enum:trainings.PlansType", "individual,personal,group");

            migrationBuilder.CreateTable(
                name: "ExerciseInfo",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BodyElements = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy_Id = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy_FullName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseInfo", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "PlansEntity",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false),
                    Image = table.Column<PlansImage>(type: "trainings.\"PlansImage\"", nullable: false),
                    ModifiedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy_Id = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy_FullName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlansEntity", x => x.Identifier);
                });

            migrationBuilder.CreateTable(
                name: "DoneExercisesEntity",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Reps = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    RPE = table.Column<int>(type: "integer", nullable: false),
                    BrakeSeconds = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<LocalDate>(type: "date", nullable: false),
                    ModifiedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy_Id = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy_FullName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoneExercisesEntity", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_DoneExercisesEntity_ExerciseInfo_ExerciseInfoId",
                        column: x => x.ExerciseInfoId,
                        principalSchema: "trainings",
                        principalTable: "ExerciseInfo",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseRecordsRegistry",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LatestRevision = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseRecordsRegistry", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_ExerciseRecordsRegistry_ExerciseInfo_ExerciseId",
                        column: x => x.ExerciseId,
                        principalSchema: "trainings",
                        principalTable: "ExerciseInfo",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    PhotoId = table.Column<string>(type: "text", nullable: false),
                    ExerciseInfoIdentifier = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Files_ExerciseInfo_ExerciseInfoIdentifier",
                        column: x => x.ExerciseInfoIdentifier,
                        principalSchema: "trainings",
                        principalTable: "ExerciseInfo",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlannedExercisesEntity",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    PlansId = table.Column<long>(type: "bigint", nullable: false),
                    ExerciseInfoId = table.Column<long>(type: "bigint", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Reps = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    RPE = table.Column<int>(type: "integer", nullable: false),
                    BrakeSeconds = table.Column<int>(type: "integer", nullable: false),
                    ModifiedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy_Id = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy_FullName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedExercisesEntity", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_PlannedExercisesEntity_ExerciseInfo_ExerciseInfoId",
                        column: x => x.ExerciseInfoId,
                        principalSchema: "trainings",
                        principalTable: "ExerciseInfo",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlannedExercisesEntity_PlansEntity_PlansId",
                        column: x => x.PlansId,
                        principalSchema: "trainings",
                        principalTable: "PlansEntity",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlannedTrainingsEntity",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<Guid>(type: "uuid", nullable: false),
                    PlansId = table.Column<long>(type: "bigint", nullable: false),
                    PlansType = table.Column<PlansType>(type: "trainings.\"PlansType\"", nullable: false),
                    DateStart = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    DateEnd = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    TrainerId = table.Column<long>(type: "bigint", nullable: true),
                    GroupId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedBy_Id = table.Column<string>(type: "text", nullable: true),
                    ModifiedBy_FullName = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlannedTrainingsEntity", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_PlannedTrainingsEntity_PlansEntity_PlansId",
                        column: x => x.PlansId,
                        principalSchema: "trainings",
                        principalTable: "PlansEntity",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseRecords",
                schema: "trainings",
                columns: table => new
                {
                    Identifier = table.Column<long>(type: "bigint", nullable: false),
                    Revision = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<LocalDate>(type: "date", nullable: false),
                    Reps = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    LombardiResult = table.Column<double>(type: "double precision", nullable: false),
                    BrzyckiResult = table.Column<double>(type: "double precision", nullable: false),
                    EpleyResult = table.Column<double>(type: "double precision", nullable: false),
                    MayhewResult = table.Column<double>(type: "double precision", nullable: false),
                    AdamsResult = table.Column<double>(type: "double precision", nullable: false),
                    BaechleResult = table.Column<double>(type: "double precision", nullable: false),
                    BergerResult = table.Column<double>(type: "double precision", nullable: false),
                    BrownResult = table.Column<double>(type: "double precision", nullable: false),
                    OneRepetitionMaximum = table.Column<double>(type: "double precision", nullable: false),
                    isAutomat = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<LocalDateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy_Id = table.Column<string>(type: "text", nullable: false),
                    CreatedBy_FullName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseRecords", x => new { x.Identifier, x.Revision });
                    table.ForeignKey(
                        name: "FK_ExerciseRecords_ExerciseRecordsRegistry_Identifier",
                        column: x => x.Identifier,
                        principalSchema: "trainings",
                        principalTable: "ExerciseRecordsRegistry",
                        principalColumn: "Identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoneExercisesEntity_ExerciseInfoId",
                schema: "trainings",
                table: "DoneExercisesEntity",
                column: "ExerciseInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseRecordsRegistry_ExerciseId",
                schema: "trainings",
                table: "ExerciseRecordsRegistry",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ExerciseInfoIdentifier",
                schema: "trainings",
                table: "Files",
                column: "ExerciseInfoIdentifier");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedExercisesEntity_ExerciseInfoId",
                schema: "trainings",
                table: "PlannedExercisesEntity",
                column: "ExerciseInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedExercisesEntity_PlansId",
                schema: "trainings",
                table: "PlannedExercisesEntity",
                column: "PlansId");

            migrationBuilder.CreateIndex(
                name: "IX_PlannedTrainingsEntity_PlansId",
                schema: "trainings",
                table: "PlannedTrainingsEntity",
                column: "PlansId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoneExercisesEntity",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "ExerciseRecords",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "Files",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "PlannedExercisesEntity",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "PlannedTrainingsEntity",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "ExerciseRecordsRegistry",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "PlansEntity",
                schema: "trainings");

            migrationBuilder.DropTable(
                name: "ExerciseInfo",
                schema: "trainings");
        }
    }
}
