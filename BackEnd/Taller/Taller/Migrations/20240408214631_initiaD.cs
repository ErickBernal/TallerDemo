using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Taller.Migrations
{
    /// <inheritdoc />
    public partial class initiaD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pwd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Work = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceWork = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeClients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleParts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceListWorks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    ServiceWorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceListWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceListWorks_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceListWorks_ServiceWorks_ServiceWorkId",
                        column: x => x.ServiceWorkId,
                        principalTable: "ServiceWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleLineas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Line = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleModelId = table.Column<int>(type: "int", nullable: false),
                    VehicleBrandId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleLineas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleLineas_VehicleBrands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleLineas_VehicleModels_VehicleModelId",
                        column: x => x.VehicleModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Municipalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Municipalities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Municipalities_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Placa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleLineaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleLineas_VehicleLineaId",
                        column: x => x.VehicleLineaId,
                        principalTable: "VehicleLineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VpartsCompatibles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleLineaId = table.Column<int>(type: "int", nullable: false),
                    VehiclePartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VpartsCompatibles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VpartsCompatibles_VehicleLineas_VehicleLineaId",
                        column: x => x.VehicleLineaId,
                        principalTable: "VehicleLineas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VpartsCompatibles_VehicleParts_VehiclePartId",
                        column: x => x.VehiclePartId,
                        principalTable: "VehicleParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DPI = table.Column<int>(type: "int", nullable: false),
                    Nit = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Cellphone = table.Column<int>(type: "int", nullable: false),
                    TypeClientId = table.Column<int>(type: "int", nullable: false),
                    Zone = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MunicipalityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Municipalities_MunicipalityId",
                        column: x => x.MunicipalityId,
                        principalTable: "Municipalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clients_TypeClients_TypeClientId",
                        column: x => x.TypeClientId,
                        principalTable: "TypeClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDetalles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false),
                    Km = table.Column<int>(type: "int", nullable: false),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDetalles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceDetalles_ServiceTypes_ServiceTypeId",
                        column: x => x.ServiceTypeId,
                        principalTable: "ServiceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceDetalles_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleClientServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceDetalleId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleClientServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleClientServices_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleClientServices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleClientServices_ServiceDetalles_ServiceDetalleId",
                        column: x => x.ServiceDetalleId,
                        principalTable: "ServiceDetalles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleVehicleParts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiclePartId = table.Column<int>(type: "int", nullable: false),
                    ServiceDetalleId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleVehicleParts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleVehicleParts_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVehicleParts_ServiceDetalles_ServiceDetalleId",
                        column: x => x.ServiceDetalleId,
                        principalTable: "ServiceDetalles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleVehicleParts_VehicleParts_VehiclePartId",
                        column: x => x.VehiclePartId,
                        principalTable: "VehicleParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetalleWorkServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceWorkId = table.Column<int>(type: "int", nullable: false),
                    ServiceDetalleId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetalleWorkServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetalleWorkServices_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleWorkServices_ServiceDetalles_ServiceDetalleId",
                        column: x => x.ServiceDetalleId,
                        principalTable: "ServiceDetalles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DetalleWorkServices_ServiceWorks_ServiceWorkId",
                        column: x => x.ServiceWorkId,
                        principalTable: "ServiceWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Guatemala" },
                    { 2, "El Salvador" },
                    { 3, "Honduras" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTypes",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Individual" },
                    { 2, "Empresa" }
                });

            migrationBuilder.InsertData(
                table: "ServiceWorks",
                columns: new[] { "Id", "PriceWork", "Work" },
                values: new object[,]
                {
                    { 1, 300, "Servicio Menor" },
                    { 2, 400, "Servicio Recurrente" },
                    { 3, 500, "Servicio Mayor" },
                    { 4, 1000, "Servicio por flotilla" }
                });

            migrationBuilder.InsertData(
                table: "TypeClients",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Individual" },
                    { 2, "Empresa" }
                });

            migrationBuilder.InsertData(
                table: "VehicleBrands",
                columns: new[] { "Id", "Brand" },
                values: new object[,]
                {
                    { 1, "Toyota" },
                    { 2, "Susuki" },
                    { 3, "Kia" },
                    { 4, "Subaru" }
                });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "Model" },
                values: new object[,]
                {
                    { 1, 2000 },
                    { 2, 2001 },
                    { 3, 2002 },
                    { 4, 2003 },
                    { 5, 2004 },
                    { 6, 2006 },
                    { 7, 2011 },
                    { 8, 2020 }
                });

            migrationBuilder.InsertData(
                table: "VehicleParts",
                columns: new[] { "Id", "Name", "Stock", "UnitPrice" },
                values: new object[,]
                {
                    { 1, "Aceite 10 w 40", 10, 100.0 },
                    { 2, "Aceite 20 w 50", 9, 99.0 },
                    { 3, "Llanta Rin 22", 12, 400.0 },
                    { 4, "Llanta Rin 17", 4, 250.0 },
                    { 5, "Plumilla", 25, 50.0 },
                    { 6, "Pastillas de freno", 10, 200.0 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CountryId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Guatemala" },
                    { 2, 1, "El Progreso" },
                    { 3, 1, "Sacatepéquez" },
                    { 4, 1, "Chimaltenango" },
                    { 5, 1, "Escuintla" },
                    { 6, 1, "Santa Rosa" },
                    { 7, 1, "Solola" },
                    { 8, 1, "Totonicapan" },
                    { 9, 1, "Quetzaltenango" },
                    { 10, 1, "Suchitepequez" },
                    { 11, 1, "Retalhuleu" },
                    { 12, 1, "San Marcos" },
                    { 13, 1, "Huehuetenango" },
                    { 14, 1, "Quiche" },
                    { 15, 1, "Baja Verapaz" },
                    { 16, 1, "Alta Verapaz" },
                    { 17, 1, "Peten" },
                    { 18, 1, "Izabal" },
                    { 19, 1, "Zacapa" },
                    { 20, 1, "Chiquimula" },
                    { 21, 1, "Jalapa" },
                    { 22, 1, "Jutiapa" },
                    { 23, 2, "Ahuachapán" },
                    { 24, 2, "Cabañas" },
                    { 25, 2, "Chalatenango" },
                    { 26, 2, "Cuscatlán" },
                    { 27, 2, "La Libertad" },
                    { 28, 2, "La Paz" },
                    { 29, 2, "La Unión" },
                    { 30, 2, "Morazán" },
                    { 31, 2, "San Miguel" },
                    { 32, 2, "San Salvador" },
                    { 33, 2, "San Vicente" },
                    { 34, 2, "Santa Ana" },
                    { 35, 2, "Sonsonate" },
                    { 36, 2, "Usulután" },
                    { 37, 3, "Atlántida" },
                    { 38, 3, "Choluteca" },
                    { 39, 3, "Colón" },
                    { 40, 3, "Comayagua" },
                    { 41, 3, "Copán" },
                    { 42, 3, "Cortés" },
                    { 43, 3, "El Paraíso" },
                    { 44, 3, "Francisco Morazán" },
                    { 45, 3, "Gracias a Dios" },
                    { 46, 3, "Intibucá" },
                    { 47, 3, "Islas de la Bahía" },
                    { 48, 3, "La Paz" },
                    { 49, 3, "Lempira" },
                    { 50, 3, "Ocotepeque" },
                    { 51, 3, "Olancho" },
                    { 52, 3, "Santa Bárbara" },
                    { 53, 3, "Valle" },
                    { 54, 3, "Yoro" }
                });

            migrationBuilder.InsertData(
                table: "VehicleLineas",
                columns: new[] { "Id", "Color", "Line", "Type", "VehicleBrandId", "VehicleModelId" },
                values: new object[,]
                {
                    { 1, "Negro", "Tacoma", "pick up", 1, 1 },
                    { 2, "Azul", "Yaris", "cedan", 1, 1 },
                    { 3, "verde", "Carry", "pick up", 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "Municipalities",
                columns: new[] { "Id", "DepartmentId", "Name" },
                values: new object[,]
                {
                    { 1, 16, "Cobán" },
                    { 2, 16, "San Pedro Carchá" },
                    { 3, 16, "San Juan Chamelco" },
                    { 4, 16, "Tactic" },
                    { 5, 16, "San Cristóbal Verapaz" },
                    { 6, 16, "Santa Cruz Verapaz" },
                    { 7, 16, "Tamahú" },
                    { 8, 16, "Tucurú" },
                    { 9, 16, "Senahú" },
                    { 10, 16, "Santa Catalina La Tinta" },
                    { 11, 16, "Santa María Cahabón" },
                    { 12, 16, "Lanquín" },
                    { 13, 16, "Chahal" },
                    { 14, 16, "San Miguel Tucurú" },
                    { 15, 16, "Chisec" },
                    { 16, 15, "Salamá" },
                    { 17, 15, "San Miguel Chicaj" },
                    { 18, 15, "Rabinal" },
                    { 19, 15, "Cubulco" },
                    { 20, 15, "Granados" },
                    { 21, 15, "Santa Cruz El Chol" },
                    { 22, 4, "Chimaltenango" },
                    { 23, 4, "San José Poaquil" },
                    { 24, 4, "San Martín Jilotepeque" },
                    { 25, 4, "San Juan Comalapa" },
                    { 26, 4, "Santa Apolonia" },
                    { 27, 4, "Tecpán Guatemala" },
                    { 28, 4, "Patzún" },
                    { 29, 4, "Pochuta" },
                    { 30, 4, "Santa Cruz Balanyá" },
                    { 31, 4, "Acatenango" },
                    { 32, 4, "Yepocapa" },
                    { 33, 4, "San Andrés Itzapa" },
                    { 34, 4, "Parramos" },
                    { 35, 4, "Zaragoza" },
                    { 36, 4, "El Tejar" },
                    { 37, 4, "San Juan Sacatepéquez" },
                    { 38, 4, "Santo Domingo Xenacoj" },
                    { 39, 4, "Santiago Sacatepéquez" },
                    { 40, 4, "San Bartolomé Milpas Altas" },
                    { 41, 4, "San Lucas Sacatepéquez" },
                    { 42, 4, "Santa Lucia Milpas Altas" },
                    { 43, 4, "Magdalena Milpas Altas" },
                    { 44, 4, "Santa María de Jesús" },
                    { 45, 21, "Jalapa" },
                    { 46, 21, "San Pedro Pinula" },
                    { 47, 21, "San Luis Jilotepeque" },
                    { 48, 21, "San Manuel Chaparrón" },
                    { 49, 21, "San Carlos Alzatate" },
                    { 50, 21, "Monjas" },
                    { 51, 21, "Mataquescuintla" },
                    { 52, 22, "Jutiapa" },
                    { 53, 22, "El Progreso" },
                    { 54, 22, "Santa Catarina Mita" },
                    { 55, 22, "Agua Blanca" },
                    { 56, 22, "Asunción Mita" },
                    { 57, 22, "Yupiltepeque" },
                    { 58, 22, "Atiquizaya" },
                    { 59, 22, "Jerez" },
                    { 60, 22, "El Adelanto" },
                    { 61, 22, "Zapotitlán" },
                    { 62, 22, "Comapa" },
                    { 63, 22, "Jalpatagua" },
                    { 64, 22, "Conguaco" },
                    { 65, 22, "Moyuta" },
                    { 66, 22, "Pasaco" },
                    { 67, 22, "San José Acatempa" },
                    { 68, 22, "Quesada" },
                    { 69, 20, "Chiquimula" },
                    { 70, 20, "San José La Arada" },
                    { 71, 20, "San Juan Ermita" },
                    { 72, 20, "Jocotán" },
                    { 73, 20, "Camotán" },
                    { 74, 20, "Olopa" },
                    { 75, 20, "Esquipulas" },
                    { 76, 20, "Quezaltepeque" },
                    { 77, 20, "Concepción Las Minas" },
                    { 78, 20, "Ipala" },
                    { 79, 19, "Zacapa" },
                    { 80, 19, "Estanzuela" },
                    { 81, 19, "Río Hondo" },
                    { 82, 19, "Gualán" },
                    { 83, 19, "Teculután" },
                    { 84, 19, "Usumatlán" },
                    { 85, 19, "Cabañas" },
                    { 86, 19, "San Diego" },
                    { 87, 19, "La Unión" },
                    { 88, 3, "Antigua Guatemala" },
                    { 89, 3, "Jocotenango" },
                    { 90, 3, "Ciudad Vieja" },
                    { 91, 3, "San Miguel Dueñas" },
                    { 92, 3, "Pastores" },
                    { 93, 3, "Santiago Sacatepéquez" },
                    { 94, 3, "Santo Domingo Xenacoj" },
                    { 95, 3, "San Bartolomé Milpas Altas" },
                    { 96, 3, "Santa Lucía Milpas Altas" },
                    { 97, 3, "Magdalena Milpas Altas" },
                    { 98, 3, "Santa María de Jesús" },
                    { 99, 3, "Santa Catarina Barahona" },
                    { 100, 3, "Alotenango" },
                    { 101, 3, "San Antonio Aguas Calientes" },
                    { 102, 3, "Santa Catarina Barahona" },
                    { 103, 3, "Alotenango" },
                    { 104, 13, "Huehuetenango" },
                    { 105, 13, "Chiantla" },
                    { 106, 13, "Malacatancito" },
                    { 107, 13, "Cuilco" },
                    { 108, 13, "Nentón" },
                    { 109, 13, "San Pedro Necta" },
                    { 110, 13, "Jacaltenango" },
                    { 111, 13, "San Pedro Soloma" },
                    { 112, 13, "San Ildefonso Ixtahuacán" },
                    { 113, 13, "Santa Bárbara" },
                    { 114, 13, "La Libertad" },
                    { 115, 13, "La Democracia" },
                    { 116, 13, "San Miguel Acatán" },
                    { 117, 13, "San Rafael La Independencia" },
                    { 118, 13, "Todos Santos Cuchumatán" },
                    { 119, 13, "San Juan Atitán" },
                    { 120, 13, "Santa Eulalia" },
                    { 121, 13, "San Mateo Ixtatán" },
                    { 122, 13, "Colotenango" },
                    { 123, 13, "San Sebastián Huehuetenango" },
                    { 124, 13, "Tectitán" },
                    { 125, 13, "Concepción Huista" },
                    { 126, 13, "San Juan Ixcoy" },
                    { 127, 13, "San Antonio Huista" },
                    { 128, 13, "San Sebastián Coatán" },
                    { 129, 13, "Barillas" },
                    { 130, 13, "Acul" },
                    { 131, 13, "Soloma" },
                    { 132, 13, "Aguacatán" },
                    { 133, 13, "San Rafael Petzal" },
                    { 134, 13, "San Gaspar Ixchil" },
                    { 135, 13, "Santiago Chimaltenango" },
                    { 136, 13, "Santa Ana Huista" },
                    { 137, 14, "Santa Cruz del Quiché" },
                    { 138, 14, "Chichicastenango" },
                    { 139, 14, "San Andrés Sajcabajá" },
                    { 140, 14, "Uspantán" },
                    { 141, 14, "Sacapulas" },
                    { 142, 14, "San Bartolomé Jocotenango" },
                    { 143, 14, "Canillá" },
                    { 144, 14, "Chajul" },
                    { 145, 14, "Chinique" },
                    { 146, 14, "Cunén" },
                    { 147, 14, "Ixcán" },
                    { 148, 14, "Pachalum" },
                    { 149, 14, "Chiché" },
                    { 150, 14, "San Pedro Necta" },
                    { 151, 14, "San Gaspar Chajul" },
                    { 152, 5, "Escuintla" },
                    { 153, 5, "Santa Lucía Cotzumalguapa" },
                    { 154, 5, "La Democracia" },
                    { 155, 5, "Siquinalá" },
                    { 156, 5, "Masagua" },
                    { 157, 5, "Tiquisate" },
                    { 158, 5, "La Gomera" },
                    { 159, 5, "Guanagazapa" },
                    { 160, 5, "San José" },
                    { 161, 5, "Iztapa" },
                    { 162, 5, "Palín" },
                    { 163, 5, "San Vicente Pacaya" },
                    { 164, 5, "Nueva Concepción" },
                    { 165, 5, "Santa Catarina Pinula" },
                    { 166, 5, "Santa Teresa" },
                    { 167, 5, "La Reforma" },
                    { 168, 5, "Pueblo Nuevo Tiquisate" },
                    { 169, 5, "Taxisco" },
                    { 170, 1, "Guatemala" },
                    { 171, 1, "Santa Catarina Pinula" },
                    { 172, 1, "San José Pinula" },
                    { 173, 1, "San José del Golfo" },
                    { 174, 1, "Palencia" },
                    { 175, 1, "Chinautla" },
                    { 176, 1, "San Pedro Ayampuc" },
                    { 177, 1, "Mixco" },
                    { 178, 1, "San Pedro Sacatepéquez" },
                    { 179, 1, "San Juan Sacatepéquez" },
                    { 180, 1, "San Raymundo" },
                    { 181, 1, "Chuarrancho" },
                    { 182, 1, "Fraijanes" },
                    { 183, 1, "Amatitlán" },
                    { 184, 1, "Villa Nueva" },
                    { 185, 1, "Villa Canales" },
                    { 186, 1, "Petapa" },
                    { 187, 12, "San Marcos" },
                    { 188, 12, "San Pedro Sacatepéquez" },
                    { 189, 12, "San Antonio Sacatepéquez" },
                    { 190, 12, "Comitancillo" },
                    { 191, 12, "San Miguel Ixtahuacán" },
                    { 192, 12, "Concepción Tutuapa" },
                    { 193, 12, "Tacaná" },
                    { 194, 12, "Sibinal" },
                    { 195, 12, "Tajumulco" },
                    { 196, 12, "Tejutla" },
                    { 197, 12, "San Rafael Pie de la Cuesta" },
                    { 198, 12, "Nuevo Progreso" },
                    { 199, 12, "El Tumbador" },
                    { 200, 12, "Malacatán" },
                    { 201, 12, "Catarina" },
                    { 202, 12, "Ayutla" },
                    { 203, 12, "Ocos" },
                    { 204, 12, "San Pablo" },
                    { 205, 12, "El Rodeo" },
                    { 206, 12, "Ixchiguan" },
                    { 207, 12, "San José Ojetenam" },
                    { 208, 12, "San Cristóbal Cucho" },
                    { 209, 12, "Sipacapa" },
                    { 210, 12, "Esquipulas Palo Gordo" },
                    { 211, 12, "Río Blanco" },
                    { 212, 12, "San Lorenzo" },
                    { 213, 7, "Sololá" },
                    { 214, 7, "San José Chacayá" },
                    { 215, 7, "Santa María Visitación" },
                    { 216, 7, "Santa Lucía Utatlán" },
                    { 217, 7, "Nahualá" },
                    { 218, 7, "Santa Catarina Ixtahuacán" },
                    { 219, 7, "Santa Clara La Laguna" },
                    { 220, 7, "Concepción" },
                    { 221, 7, "San Andrés Semetabaj" },
                    { 222, 7, "Panajachel" },
                    { 223, 7, "Santa Catarina Palopó" },
                    { 224, 7, "San Antonio Palopó" },
                    { 225, 7, "San Lucas Tolimán" },
                    { 226, 7, "Santa Cruz La Laguna" },
                    { 227, 7, "San Marcos La Laguna" },
                    { 228, 7, "San Juan La Laguna" },
                    { 229, 7, "San Pablo La Laguna" },
                    { 230, 7, "San Pedro La Laguna" },
                    { 231, 7, "Santiago Atitlán" },
                    { 232, 10, "Mazatenango" },
                    { 233, 10, "Cuyotenango" },
                    { 234, 10, "San Francisco Zapotitlán" },
                    { 235, 10, "San Bernardino" },
                    { 236, 10, "San José El Ídolo" },
                    { 237, 10, "San Antonio Suchitepéquez" },
                    { 238, 10, "Santo Domingo Suchitepéquez" },
                    { 239, 10, "San Lorenzo" },
                    { 240, 10, "Samayac" },
                    { 241, 10, "Patulul" },
                    { 242, 10, "San Pablo Jocopilas" },
                    { 243, 10, "San Antonio Suchitepéquez" },
                    { 244, 10, "Santo Domingo Suchitepéquez" },
                    { 245, 10, "San Lorenzo" },
                    { 246, 10, "Samayac" },
                    { 247, 10, "Patulul" },
                    { 248, 10, "San Pablo Jocopilas" },
                    { 249, 10, "San Antonio Suchitepéquez" },
                    { 250, 10, "Santo Domingo Suchitepéquez" },
                    { 251, 10, "San Lorenzo" },
                    { 252, 10, "Samayac" },
                    { 253, 10, "Patulul" },
                    { 254, 10, "San Pablo Jocopilas" },
                    { 255, 10, "San Antonio Suchitepéquez" },
                    { 256, 10, "Santo Domingo Suchitepéquez" },
                    { 257, 10, "San Lorenzo" },
                    { 258, 10, "Samayac" },
                    { 259, 10, "Patulul" },
                    { 260, 10, "San Pablo Jocopilas" },
                    { 261, 10, "San Antonio Suchitepéquez" },
                    { 262, 10, "Santo Domingo Suchitepéquez" },
                    { 263, 10, "San Lorenzo" },
                    { 264, 10, "Samayac" },
                    { 265, 10, "Patulul" },
                    { 266, 10, "San Pablo Jocopilas" },
                    { 267, 10, "San Antonio Suchitepéquez" },
                    { 268, 10, "Santo Domingo Suchitepéquez" },
                    { 269, 8, "Totonicapán" },
                    { 270, 8, "San Cristóbal Totonicapán" },
                    { 271, 8, "San Francisco El Alto" },
                    { 272, 8, "San Andrés Xecul" },
                    { 273, 8, "Momostenango" },
                    { 274, 8, "Santa María Chiquimula" },
                    { 275, 8, "Santa Lucía La Reforma" },
                    { 276, 8, "San Bartolo" },
                    { 277, 9, "Quetzaltenango" },
                    { 278, 9, "Salcajá" },
                    { 279, 9, "Olintepeque" },
                    { 280, 9, "San Carlos Sija" },
                    { 281, 9, "Sibilia" },
                    { 282, 9, "Cantel" },
                    { 283, 9, "Huitán" },
                    { 284, 9, "Zunil" },
                    { 285, 9, "Colomba" },
                    { 286, 9, "San Francisco La Unión" },
                    { 287, 9, "El Palmar" },
                    { 288, 9, "Concepción Chiquirichapa" },
                    { 289, 9, "San Mateo" },
                    { 290, 9, "La Esperanza" },
                    { 291, 9, "Flores Costa Cuca" },
                    { 292, 9, "Génova" },
                    { 293, 9, "Palestina de los Altos" },
                    { 294, 9, "Coatepeque" },
                    { 295, 9, "Flores Costa Cuca" },
                    { 296, 9, "Génova" },
                    { 297, 9, "Palestina de los Altos" },
                    { 298, 9, "La Esperanza" },
                    { 299, 9, "Flores Costa Cuca" },
                    { 300, 9, "Génova" },
                    { 301, 9, "Palestina de los Altos" },
                    { 302, 9, "San Carlos Sija" },
                    { 303, 9, "Sibilia" },
                    { 304, 9, "Cantel" },
                    { 305, 9, "Huitán" },
                    { 306, 9, "Zunil" },
                    { 307, 9, "Colomba" },
                    { 308, 9, "San Francisco La Unión" },
                    { 309, 9, "El Palmar" },
                    { 310, 9, "Concepción Chiquirichapa" },
                    { 311, 9, "San Mateo" },
                    { 312, 9, "La Esperanza" },
                    { 313, 9, "Flores Costa Cuca" },
                    { 314, 9, "Génova" },
                    { 315, 9, "Palestina de los Altos" },
                    { 316, 9, "Coatepeque" },
                    { 317, 9, "Flores Costa Cuca" },
                    { 318, 9, "Génova" },
                    { 319, 9, "Palestina de los Altos" },
                    { 320, 9, "La Esperanza" },
                    { 321, 9, "Flores Costa Cuca" },
                    { 322, 9, "Génova" },
                    { 323, 9, "Palestina de los Altos" },
                    { 324, 9, "La Esperanza" },
                    { 325, 9, "Flores Costa Cuca" },
                    { 326, 9, "Génova" },
                    { 327, 9, "Palestina de los Altos" },
                    { 328, 9, "La Esperanza" },
                    { 329, 9, "Flores Costa Cuca" },
                    { 330, 9, "Génova" },
                    { 331, 9, "Palestina de los Altos" },
                    { 332, 23, "Ahuachapán" },
                    { 333, 23, "Apaneca" },
                    { 334, 23, "Atiquizaya" },
                    { 335, 23, "Concepción de Ataco" },
                    { 336, 23, "El Refugio" },
                    { 337, 23, "Guaymango" },
                    { 338, 23, "Jujutla" },
                    { 339, 23, "San Francisco Menéndez" },
                    { 340, 23, "San Lorenzo" },
                    { 341, 23, "San Pedro Puxtla" },
                    { 342, 23, "Turín" },
                    { 343, 23, "La Concordia" },
                    { 344, 23, "El Sauce" },
                    { 345, 23, "Tacuba" },
                    { 346, 24, "Sensuntepeque" },
                    { 347, 24, "Dolores" },
                    { 348, 24, "Ilobasco" },
                    { 349, 24, "Victoria" },
                    { 350, 24, "Guacotecti" },
                    { 351, 24, "San Isidro" },
                    { 352, 24, "Tejutepeque" },
                    { 353, 24, "Jutiapa" },
                    { 354, 25, "Chalatenango" },
                    { 355, 25, "Arcatao" },
                    { 356, 25, "Azacualpa" },
                    { 357, 25, "Comalapa" },
                    { 358, 25, "Citalá" },
                    { 359, 25, "Concepción Quezaltepeque" },
                    { 360, 25, "La Laguna" },
                    { 361, 25, "La Palma" },
                    { 362, 25, "La Reina" },
                    { 363, 25, "San Antonio Los Ranchos" },
                    { 364, 25, "San Antonio de la Cruz" },
                    { 365, 25, "San Fernando" },
                    { 366, 25, "San Francisco Lempa" },
                    { 367, 25, "San Francisco Morazán" },
                    { 368, 25, "San Ignacio" },
                    { 369, 25, "San Isidro Labrador" },
                    { 370, 25, "San José Cancasque" },
                    { 371, 25, "San José Las Flores" },
                    { 372, 25, "San Luis La Herradura" },
                    { 373, 25, "San Luis Talpa" },
                    { 374, 25, "San Miguel Tepezontes" },
                    { 375, 25, "San Pedro Masahuat" },
                    { 376, 25, "San Pedro Nonualco" },
                    { 377, 25, "San Rafael Obrajuelo" },
                    { 378, 25, "Santa María Ostuma" },
                    { 379, 25, "Santiago Nonualco" },
                    { 380, 25, "Tapalhuaca" },
                    { 381, 26, "Cojutepeque" },
                    { 382, 26, "Candelaria" },
                    { 383, 26, "El Carmen" },
                    { 384, 26, "El Rosario" },
                    { 385, 26, "Monte San Juan" },
                    { 386, 26, "Oratorio de Concepción" },
                    { 387, 26, "Suchitoto" },
                    { 388, 26, "Tecoluca" },
                    { 389, 26, "Victoria" },
                    { 390, 27, "Chiltiupán" },
                    { 391, 27, "Ciudad Arce" },
                    { 392, 27, "Colón" },
                    { 393, 27, "Comasagua" },
                    { 394, 27, "Huizúcar" },
                    { 395, 27, "Jayaque" },
                    { 396, 27, "Jicalapa" },
                    { 397, 27, "La Libertad" },
                    { 398, 27, "Nuevo Cuscatlán" },
                    { 399, 27, "Opico" },
                    { 400, 27, "Quezaltepeque" },
                    { 401, 27, "Sacacoyo" },
                    { 402, 27, "San José Villanueva" },
                    { 403, 27, "San Matías" },
                    { 404, 27, "San Pablo Tacachico" },
                    { 405, 27, "Santa Tecla" },
                    { 406, 27, "Talnique" },
                    { 407, 27, "Tamanique" },
                    { 408, 27, "Teotepeque" },
                    { 409, 27, "Tepecoyo" },
                    { 410, 27, "Zaragoza" },
                    { 411, 28, "La Paz" },
                    { 412, 28, "Cuyultitán" },
                    { 413, 28, "El Rosario" },
                    { 414, 28, "Jerusalén" },
                    { 415, 28, "Mercedes La Ceiba" },
                    { 416, 28, "Olocuilta" },
                    { 417, 28, "Paraíso de Osorio" },
                    { 418, 28, "San Antonio Masahuat" },
                    { 419, 28, "San Emigdio" },
                    { 420, 28, "San Francisco Chinameca" },
                    { 421, 28, "San Juan Nonualco" },
                    { 422, 28, "San Juan Talpa" },
                    { 423, 28, "San Juan Tepezontes" },
                    { 424, 28, "San Luis La Herradura" },
                    { 425, 28, "San Luis Talpa" },
                    { 426, 28, "San Miguel Tepezontes" },
                    { 427, 28, "San Pedro Masahuat" },
                    { 428, 28, "San Pedro Nonualco" },
                    { 429, 28, "San Rafael Obrajuelo" },
                    { 430, 28, "Santa María Ostuma" },
                    { 431, 28, "Santiago Nonualco" },
                    { 432, 28, "Tapalhuaca" },
                    { 433, 29, "La Unión" },
                    { 434, 29, "Anamorós" },
                    { 435, 29, "Bolívar" },
                    { 436, 29, "Concepción de Oriente" },
                    { 437, 29, "Conchagua" },
                    { 438, 29, "El Carmen" },
                    { 439, 29, "El Sauce" },
                    { 440, 29, "Intipucá" },
                    { 441, 29, "Lislique" },
                    { 442, 29, "Meanguera del Golfo" },
                    { 443, 29, "Nueva Esparta" },
                    { 444, 29, "Pasaquina" },
                    { 445, 29, "Perquín" },
                    { 446, 29, "Polorós" },
                    { 447, 29, "San Alejo" },
                    { 448, 29, "San José" },
                    { 449, 29, "Santa Rosa de Lima" },
                    { 450, 29, "Yayantique" },
                    { 451, 29, "Yucuaiquín" },
                    { 452, 30, "Guatajiagua" },
                    { 453, 30, "Joateca" },
                    { 454, 30, "Jocoaitique" },
                    { 455, 30, "Morazán" },
                    { 456, 30, "San Carlos" },
                    { 457, 30, "San Fernando" },
                    { 458, 30, "San Francisco Gotera" },
                    { 459, 30, "San Isidro" },
                    { 460, 30, "San Simón" },
                    { 461, 30, "Sociedad" },
                    { 462, 30, "Torre" },
                    { 463, 31, "San Miguel" },
                    { 464, 31, "Alegria" },
                    { 465, 31, "Carolina" },
                    { 466, 31, "Chapeltique" },
                    { 467, 31, "Chinameca" },
                    { 468, 31, "Chirilagua" },
                    { 469, 31, "Ciudad Barrios" },
                    { 470, 31, "Comacarán" },
                    { 471, 31, "El Tránsito" },
                    { 472, 31, "Lolotique" },
                    { 473, 31, "Moncagua" },
                    { 474, 31, "Nueva Guadalupe" },
                    { 475, 31, "Quelepa" },
                    { 476, 31, "San Antonio" },
                    { 477, 31, "San Gerardo" },
                    { 478, 31, "San Jorge" },
                    { 479, 31, "San Luis de la Reina" },
                    { 480, 31, "San Rafael" },
                    { 481, 31, "Sesori" },
                    { 482, 31, "Uluazapa" },
                    { 483, 32, "San Salvador" },
                    { 484, 32, "Aguilares" },
                    { 485, 32, "Apopa" },
                    { 486, 32, "Ayutuxtepeque" },
                    { 487, 32, "Cuscatancingo" },
                    { 488, 32, "Delgado" },
                    { 489, 32, "El Paisnal" },
                    { 490, 32, "Guazapa" },
                    { 491, 32, "Ilopango" },
                    { 492, 32, "Mejicanos" },
                    { 493, 32, "Nejapa" },
                    { 494, 32, "Panchimalco" },
                    { 495, 32, "San Marcos" },
                    { 496, 32, "San Martín" },
                    { 497, 32, "Santiago Texacuangos" },
                    { 498, 32, "Santo Tomás" },
                    { 499, 32, "Soyapango" },
                    { 500, 32, "Tonacatepeque" },
                    { 501, 33, "San Vicente" },
                    { 502, 33, "Azacualpa" },
                    { 503, 33, "Candelaria" },
                    { 504, 33, "Colon" },
                    { 505, 33, "Guadalupe" },
                    { 506, 33, "San Cayetano Istepeque" },
                    { 507, 33, "San Esteban Catarina" },
                    { 508, 33, "San Ildefonso" },
                    { 509, 33, "San Lorenzo" },
                    { 510, 33, "San Sebastián" },
                    { 511, 33, "Santa Clara" },
                    { 512, 33, "Santo Domingo" },
                    { 513, 33, "Tecoluca" },
                    { 514, 33, "Tepetitán" },
                    { 515, 33, "Verapaz" },
                    { 516, 34, "Santa Ana" },
                    { 517, 34, "Candelaria de la Frontera" },
                    { 518, 34, "Chalchuapa" },
                    { 519, 34, "Coatepeque" },
                    { 520, 34, "El Congo" },
                    { 521, 34, "El Porvenir" },
                    { 522, 34, "Masahuat" },
                    { 523, 34, "Metapán" },
                    { 524, 34, "San Antonio Pajonal" },
                    { 525, 34, "San Sebastián Salitrillo" },
                    { 526, 34, "Santa Rosa Guachipilín" },
                    { 527, 34, "Santiago de la Frontera" },
                    { 528, 34, "Texistepeque" },
                    { 529, 35, "Sonsonate" },
                    { 530, 35, "Acajutla" },
                    { 531, 35, "Armenia" },
                    { 532, 35, "Caluco" },
                    { 533, 35, "Cuisnahuat" },
                    { 534, 35, "Izalco" },
                    { 535, 35, "Jayaque" },
                    { 536, 35, "Juayúa" },
                    { 537, 35, "Nahuizalco" },
                    { 538, 35, "Nahulingo" },
                    { 539, 35, "Salamá" },
                    { 540, 35, "Sta. Catarina Masahuat" },
                    { 541, 35, "Sta. Isabel Ishuatán" },
                    { 542, 35, "Sensuntepeque" },
                    { 543, 35, "Sonsonate" },
                    { 544, 35, "Sonzacate" },
                    { 545, 36, "Usulután" },
                    { 546, 36, "Alegría" },
                    { 547, 36, "Berlín" },
                    { 548, 36, "California" },
                    { 549, 36, "Concepción Batres" },
                    { 550, 36, "El Triunfo" },
                    { 551, 36, "Ereguayquín" },
                    { 552, 36, "Estanzuelas" },
                    { 553, 36, "Jiquilisco" },
                    { 554, 36, "Jucuapa" },
                    { 555, 36, "Jucuarán" },
                    { 556, 36, "Mercedes Umaña" },
                    { 557, 36, "Nueva Granada" },
                    { 558, 36, "Ozatlán" },
                    { 559, 36, "Pueblo Nuevo" },
                    { 560, 36, "San Agustín" },
                    { 561, 36, "San Buenaventura" },
                    { 562, 36, "San Dionisio" },
                    { 563, 36, "San Francisco Javier" },
                    { 564, 36, "Santa Elena" },
                    { 565, 36, "Santa María" },
                    { 566, 37, "Arizona" },
                    { 567, 37, "El Porvenir" },
                    { 568, 37, "Esperanza" },
                    { 569, 37, "Ilama" },
                    { 570, 37, "La Ceiba" },
                    { 571, 37, "La Masica" },
                    { 572, 37, "Potrerillos" },
                    { 573, 37, "San Francisco" },
                    { 574, 37, "Tela" },
                    { 575, 37, "Arizona" },
                    { 576, 38, "Apacilagua" },
                    { 577, 38, "Choluteca" },
                    { 578, 38, "Duyure" },
                    { 579, 38, "El Corpus" },
                    { 580, 38, "El Triunfo" },
                    { 581, 38, "Morolica" },
                    { 582, 38, "Namasigüe" },
                    { 583, 38, "Orocuina" },
                    { 584, 38, "Pespire" },
                    { 585, 38, "San Antonio de Flores" },
                    { 586, 38, "San Isidro" },
                    { 587, 38, "San José" },
                    { 588, 38, "San Marcos de Colón" },
                    { 589, 38, "San Nicolás" },
                    { 590, 38, "Santa Ana de Yusguare" },
                    { 591, 38, "Santa Lucía" },
                    { 592, 38, "Yusguare" },
                    { 593, 39, "Balfate" },
                    { 594, 39, "Bonito Oriental" },
                    { 595, 39, "iriboca" },
                    { 596, 39, "El Porvenir" },
                    { 597, 39, "limones" },
                    { 598, 39, "Saba" },
                    { 599, 39, "Santa Rosa de Aguán" },
                    { 600, 39, "Sonaguera" },
                    { 601, 39, "Tocoa" },
                    { 602, 40, "Comayagua" },
                    { 603, 40, "El Rosario" },
                    { 604, 40, "Esquías" },
                    { 605, 40, "Humuya" },
                    { 606, 40, "La Libertad" },
                    { 607, 40, "Lamaní" },
                    { 608, 40, "La Trinidad" },
                    { 609, 40, "Lejamaní" },
                    { 610, 40, "Meámbar" },
                    { 611, 40, "Minas de Oro" },
                    { 612, 40, "Ojos de Agua" },
                    { 613, 40, "San Jerónimo" },
                    { 614, 40, "San José de Comayagua" },
                    { 615, 40, "San José del Potrero" },
                    { 616, 40, "San Luis" },
                    { 617, 40, "San Sebastián" },
                    { 618, 40, "Siguatepeque" },
                    { 619, 40, "Taulabé" },
                    { 620, 40, "Villa de San Antonio" },
                    { 621, 41, "Cabañas" },
                    { 622, 41, "Concepción" },
                    { 623, 41, "Copán Ruinas" },
                    { 624, 41, "Corquín" },
                    { 625, 41, "Cucuyagua" },
                    { 626, 41, "Dolores" },
                    { 627, 41, "Dulce Nombre" },
                    { 628, 41, "El Paraíso" },
                    { 629, 41, "Florida" },
                    { 630, 41, "La Jigua" },
                    { 631, 41, "La Unión" },
                    { 632, 41, "Nueva Arcadia" },
                    { 633, 41, "San Agustín" },
                    { 634, 41, "San Antonio" },
                    { 635, 41, "San Jerónimo" },
                    { 636, 41, "San José" },
                    { 637, 41, "San Juan de Opoa" },
                    { 638, 41, "San Nicolás" },
                    { 639, 41, "San Pedro" },
                    { 640, 41, "Santa Rita" },
                    { 641, 41, "Santa Rosa de Copán" },
                    { 642, 41, "Trinidad de Copán" },
                    { 643, 41, "Veracruz" },
                    { 644, 41, "Viñetas" },
                    { 645, 42, "Choloma" },
                    { 646, 42, "La Lima" },
                    { 647, 42, "Omoa" },
                    { 648, 42, "Pimienta" },
                    { 649, 42, "Puerto Cortés" },
                    { 650, 42, "San Antonio de Cortés" },
                    { 651, 42, "San Francisco de Yojoa" },
                    { 652, 42, "San Manuel" },
                    { 653, 42, "San Pedro Sula" },
                    { 654, 42, "Santa Cruz de Yojoa" },
                    { 655, 42, "Villanueva" },
                    { 656, 43, "Alauca" },
                    { 657, 43, "Danlí" },
                    { 658, 43, "El Paraíso" },
                    { 659, 43, "Güinope" },
                    { 660, 43, "Liure" },
                    { 661, 43, "Morocelí" },
                    { 662, 43, "Oropolí" },
                    { 663, 43, "Potrerillos" },
                    { 664, 43, "Reitoca" },
                    { 665, 43, "San Antonio de Flores" },
                    { 666, 43, "San Lucas" },
                    { 667, 43, "San Matías" },
                    { 668, 43, "Soledad" },
                    { 669, 43, "Taujica" },
                    { 670, 43, "Texiguat" },
                    { 671, 43, "Vado Ancho" },
                    { 672, 43, "Yauyupe" },
                    { 673, 43, "Yuscarán" },
                    { 674, 44, "Alubarén" },
                    { 675, 44, "Cedros" },
                    { 676, 44, "Distrito Central" },
                    { 677, 44, "El Porvenir" },
                    { 678, 44, "Guaimaca" },
                    { 679, 44, "La Libertad" },
                    { 680, 44, "La Venta" },
                    { 681, 44, "Lepaterique" },
                    { 682, 44, "Maraita" },
                    { 683, 44, "Marale" },
                    { 684, 44, "Nueva Armenia" },
                    { 685, 44, "Ojojona" },
                    { 686, 44, "Orica" },
                    { 687, 44, "Reitoca" },
                    { 688, 44, "Sabanagrande" },
                    { 689, 44, "San Antonio de Oriente" },
                    { 690, 44, "San Buenaventura" },
                    { 691, 44, "San Ignacio" },
                    { 692, 44, "San Juan de Flores" },
                    { 693, 44, "San Marcos de Colón" },
                    { 694, 44, "Santa Ana" },
                    { 695, 44, "Santa Lucía" },
                    { 696, 44, "Talanga" },
                    { 697, 44, "Tatumbla" },
                    { 698, 44, "Valle de Angeles" },
                    { 699, 44, "Vallecillo" },
                    { 700, 45, "Ahuas" },
                    { 701, 45, "Brus Laguna" },
                    { 702, 45, "Juan Francisco Bulnes" },
                    { 703, 45, "Puerto Lempira" },
                    { 704, 45, "Wampusirpi" },
                    { 705, 46, "Camacaro" },
                    { 706, 46, "Colomoncagua" },
                    { 707, 46, "Concepción" },
                    { 708, 46, "Dolores" },
                    { 709, 46, "Intibucá" },
                    { 710, 46, "Jesus de Otoro" },
                    { 711, 46, "La Esperanza" },
                    { 712, 46, "Magdalena" },
                    { 713, 46, "Masaguara" },
                    { 714, 46, "San Antonio" },
                    { 715, 46, "San Francisco de Opalaca" },
                    { 716, 46, "San Isidro" },
                    { 717, 46, "San Juan" },
                    { 718, 46, "San Marco de Sierra" },
                    { 719, 46, "San Miguelito" },
                    { 720, 46, "San Rafael" },
                    { 721, 46, "San Sebastián" },
                    { 722, 46, "Santa Lucía" },
                    { 723, 46, "Yamaranguila" },
                    { 724, 46, "Yauyupe" },
                    { 725, 46, "Zapotillo" },
                    { 726, 47, "Roatán" },
                    { 727, 47, "Santos Guardiola" },
                    { 728, 47, "Utila" },
                    { 729, 48, "Aguaqueterique" },
                    { 730, 48, "Alegría" },
                    { 731, 48, "Cabañas" },
                    { 732, 48, "Cane" },
                    { 733, 48, "Chinacla" },
                    { 734, 48, "Guajiquiro" },
                    { 735, 48, "La Paz" },
                    { 736, 48, "Lauterique" },
                    { 737, 48, "Marcala" },
                    { 738, 48, "Mercedes de Oriente" },
                    { 739, 48, "Opatoro" },
                    { 740, 48, "San Antonio del Norte" },
                    { 741, 48, "San José" },
                    { 742, 48, "San Juan" },
                    { 743, 48, "San Pedro de Tutule" },
                    { 744, 48, "Santa Ana" },
                    { 745, 48, "Santa Elena" },
                    { 746, 48, "Santa María" },
                    { 747, 48, "Santiago de Puringla" },
                    { 748, 48, "Yarula" },
                    { 749, 49, "Belen" },
                    { 750, 49, "Candelaria" },
                    { 751, 49, "Cololaca" },
                    { 752, 49, "Erandique" },
                    { 753, 49, "Gracias" },
                    { 754, 49, "Gualcince" },
                    { 755, 49, "Guarita" },
                    { 756, 49, "La Campa" },
                    { 757, 49, "La Iguala" },
                    { 758, 49, "Lepaera" },
                    { 759, 49, "Nueva Arcadia" },
                    { 760, 49, "San Andrés" },
                    { 761, 49, "San Francisco" },
                    { 762, 49, "San Juan Guarita" },
                    { 763, 49, "San Manuel Colohete" },
                    { 764, 49, "San Marcos de Caiquín" },
                    { 765, 49, "San Rafael" },
                    { 766, 49, "San Sebastian" },
                    { 767, 49, "Santa Cruz" },
                    { 768, 49, "Talgua" },
                    { 769, 49, "Tamarindo" },
                    { 770, 49, "Tomalá" },
                    { 771, 49, "Valladolid" },
                    { 772, 49, "Virgen" },
                    { 773, 50, "Belen Gualcho" },
                    { 774, 50, "Concepción" },
                    { 775, 50, "Dolores Merendon" },
                    { 776, 50, "Fraternidad" },
                    { 777, 50, "La Encarnación" },
                    { 778, 50, "La Labor" },
                    { 779, 50, "Lepaera" },
                    { 780, 50, "Nueva Ocotepeque" },
                    { 781, 50, "San Fernando" },
                    { 782, 50, "San Francisco del Valle" },
                    { 783, 50, "San Jorge" },
                    { 784, 50, "San Marcos" },
                    { 785, 50, "San Matías" },
                    { 786, 50, "San Pedro" },
                    { 787, 50, "Santa Fe" },
                    { 788, 50, "Sensenti" },
                    { 789, 51, "Campamento" },
                    { 790, 51, "Catacamas" },
                    { 791, 51, "Concordia" },
                    { 792, 51, "Dulce Nombre de Culmí" },
                    { 793, 51, "El Rosario" },
                    { 794, 51, "Esquipulas del Norte" },
                    { 795, 51, "Gualaco" },
                    { 796, 51, "Guarizama" },
                    { 797, 51, "Jano" },
                    { 798, 51, "Juticalpa" },
                    { 799, 51, "La Unión" },
                    { 800, 51, "Mangulile" },
                    { 801, 51, "Manto" },
                    { 802, 51, "Salamá" },
                    { 803, 51, "San Esteban" },
                    { 804, 51, "San Francisco de Becerra" },
                    { 805, 51, "San Francisco de la Paz" },
                    { 806, 51, "Santa Maria del Real" },
                    { 807, 51, "Silca" },
                    { 808, 51, "Yocón" },
                    { 809, 51, "Yoro" },
                    { 810, 52, "Arada" },
                    { 811, 52, "Atima" },
                    { 812, 52, "Azacualpa" },
                    { 813, 52, "Ceguaca" },
                    { 814, 52, "Concepción del Norte" },
                    { 815, 52, "Concepción del Sur" },
                    { 816, 52, "Chinda" },
                    { 817, 52, "El Nispero" },
                    { 818, 52, "Gualala" },
                    { 819, 52, "Ilama" },
                    { 820, 52, "Macuelizo" },
                    { 821, 52, "Naranjito" },
                    { 822, 52, "Petoa" },
                    { 823, 52, "Protección" },
                    { 824, 52, "Quimistán" },
                    { 825, 52, "San Francisco de Ojuera" },
                    { 826, 52, "San Luis" },
                    { 827, 52, "San Marcos" },
                    { 828, 52, "San Nicolás" },
                    { 829, 52, "San Pedro Zacapa" },
                    { 830, 52, "Santa Bárbara" },
                    { 831, 52, "Santa Rita" },
                    { 832, 52, "San Vicente Centenario" },
                    { 833, 52, "Trinidad" },
                    { 834, 53, "Alianza" },
                    { 835, 53, "Amapala" },
                    { 836, 53, "Aramecina" },
                    { 837, 53, "Caridad" },
                    { 838, 53, "Goascorán" },
                    { 839, 53, "Langue" },
                    { 840, 53, "Nacaome" },
                    { 841, 53, "San Francisco de Coray" },
                    { 842, 53, "San Lorenzo" },
                    { 843, 53, "San Marcos de Colón" },
                    { 844, 53, "Santa Ana" },
                    { 845, 53, "Pespire" },
                    { 846, 53, "Quebrada Seca" },
                    { 847, 53, "San Antonio de Flores" },
                    { 848, 53, "San José" },
                    { 849, 54, "Arada" },
                    { 850, 54, "El Negrito" },
                    { 851, 54, "El Progreso" },
                    { 852, 54, "Jocón" },
                    { 853, 54, "Morazán" },
                    { 854, 54, "Olanchito" },
                    { 855, 54, "Sabá" },
                    { 856, 54, "San Francisco de la Paz" },
                    { 857, 54, "Santa Rita" },
                    { 858, 54, "Sulaco" },
                    { 859, 54, "Yorito" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_MunicipalityId",
                table: "Clients",
                column: "MunicipalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_TypeClientId",
                table: "Clients",
                column: "TypeClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CountryId",
                table: "Departments",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleClientServices_ClientId",
                table: "DetalleClientServices",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleClientServices_InvoiceId",
                table: "DetalleClientServices",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleClientServices_ServiceDetalleId",
                table: "DetalleClientServices",
                column: "ServiceDetalleId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVehicleParts_InvoiceId",
                table: "DetalleVehicleParts",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVehicleParts_ServiceDetalleId",
                table: "DetalleVehicleParts",
                column: "ServiceDetalleId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleVehicleParts_VehiclePartId",
                table: "DetalleVehicleParts",
                column: "VehiclePartId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleWorkServices_InvoiceId",
                table: "DetalleWorkServices",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleWorkServices_ServiceDetalleId",
                table: "DetalleWorkServices",
                column: "ServiceDetalleId");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleWorkServices_ServiceWorkId",
                table: "DetalleWorkServices",
                column: "ServiceWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Municipalities_DepartmentId",
                table: "Municipalities",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetalles_ServiceTypeId",
                table: "ServiceDetalles",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceDetalles_VehicleId",
                table: "ServiceDetalles",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceListWorks_ServiceTypeId",
                table: "ServiceListWorks",
                column: "ServiceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceListWorks_ServiceWorkId",
                table: "ServiceListWorks",
                column: "ServiceWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLineas_VehicleBrandId",
                table: "VehicleLineas",
                column: "VehicleBrandId");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLineas_VehicleModelId",
                table: "VehicleLineas",
                column: "VehicleModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_VehicleLineaId",
                table: "Vehicles",
                column: "VehicleLineaId");

            migrationBuilder.CreateIndex(
                name: "IX_VpartsCompatibles_VehicleLineaId",
                table: "VpartsCompatibles",
                column: "VehicleLineaId");

            migrationBuilder.CreateIndex(
                name: "IX_VpartsCompatibles_VehiclePartId",
                table: "VpartsCompatibles",
                column: "VehiclePartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetalleClientServices");

            migrationBuilder.DropTable(
                name: "DetalleVehicleParts");

            migrationBuilder.DropTable(
                name: "DetalleWorkServices");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropTable(
                name: "ServiceListWorks");

            migrationBuilder.DropTable(
                name: "VpartsCompatibles");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "ServiceDetalles");

            migrationBuilder.DropTable(
                name: "ServiceWorks");

            migrationBuilder.DropTable(
                name: "VehicleParts");

            migrationBuilder.DropTable(
                name: "Municipalities");

            migrationBuilder.DropTable(
                name: "TypeClients");

            migrationBuilder.DropTable(
                name: "ServiceTypes");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "VehicleLineas");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "VehicleBrands");

            migrationBuilder.DropTable(
                name: "VehicleModels");
        }
    }
}
