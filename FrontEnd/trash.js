.
console.log(JSON.stringify(servicesDetalle));

<!-- selected: {{ JSON.stringify(rowSelected) }} -->




<div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
<CreateUpdateClient componentName="Agregar Cliente" />
</div>


[
{ "id": 2, "type": "Empresa", "clients": null },
{ "id": 1, "type": "Individual", "clients": null }
]


const getClientOptions = (row) => {
    if (row.typeClient.type === "Individual") {
      return optionsTypeClient.value.filter((option) => option.id === 1);
    } else if (row.typeClient.type === "Empresa") {
      return optionsTypeClient.value.filter((option) => option.id === 2);
    } else {
      return [];
    }
  };


  :options="optionsTypeClient"
  :option-value="'id'"
  :option-label="'type'"


  const optionLabelTemplate = (option) => {
    return `${option.type} ${option.vehicleBrand.brand} ${option.line} ${option.vehicleModel.model}`;
  };


  <div id="q-app" style="min-height: 100vh">


  const columns = [
    {
      name: "Placa",
      align: "left",
      label: "Placa",
      field: "placa",
      sortable: true,
    },
    {
      name: "type",
      align: "left",
      label: "Tipo",
      field: "vehicleLinea.type",
      sortable: true,
      format: (val, row) => {
        return row.vehicleLinea ? row.vehicleLinea.type : "";
      },
    },
    {
      name: "linea",
      align: "left",
      label: "Linea",
      field: "vehicleLinea.line",
      sortable: true,
      format: (val, row) => {
        return row.vehicleLinea ? row.vehicleLinea.line : "";
      },
    },
    {
      name: "color",
      align: "left",
      label: "Color",
      field: "vehicleLinea.color",
      sortable: true,
      format: (val, row) => {
        return row.vehicleLinea ? row.vehicleLinea.color : "";
      },
    },
  ];



  cursoVue3Sitecpro123#


  modelBuilder.Entity<DetalleVehicleParts>() // Aquí estamos configurando la entidad DetalleVehicleParts en el modelo de datos.
    .HasOne(dvp => dvp.ServiceDetalle)
     // Definimos una relación de uno a uno entre DetalleVehicleParts y ServiceDetalle. 
     Esto significa que cada DetalleVehicleParts tendrá exactamente un ServiceDetalle.

     .WithMany(sd => sd.DetalleVehicleParts) 
    
    // Este método indica que ServiceDetalle tiene una colección de DetalleVehicleParts. 
    Indica la multiplicidad de la relación. Con WithMany estamos especificando
    una relación uno-a-muchos, es decir, un ServiceDetalle puede tener varios DetalleVehicleParts.

    .HasForeignKey(dvp => dvp.ServiceDetalleId) 
    
    // Establece la clave externa (foreign key) en DetalleVehicleParts que hace 
    referencia a la clave primaria de ServiceDetalle. En este caso, estamos 
    especificando que la propiedad ServiceDetalleId de DetalleVehicleParts es 
    la clave foránea que apunta a la clave primaria de ServiceDetalle.


    .OnDelete(DeleteBehavior.Restrict);
     // Este método especifica cómo se debe comportar la 
     eliminación de filas relacionadas cuando se elimina 
     la entidad principal. En este caso, se establece en Restrict, 
     lo que significa que no se puede eliminar la entidad principal
      (ServiceDetalle) si hay filas relacionadas (DetalleVehicleParts) presentes.
       Esto previene la eliminación en cascada, donde eliminar 
       una entidad principal también eliminaría automáticamente 
       las entidades relacionadas.
