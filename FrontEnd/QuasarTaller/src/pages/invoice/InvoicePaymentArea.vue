<template>
  <q-card>
    <q-card-section class="section bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs text-center">Factura</div>
    </q-card-section>
  </q-card>
  <br />
  <q-card horizontal>
    <q-card-section class="q-pt-xs col-4">
      <div class="row q-col-gutter-sm q-ma-xs">
        <div
          class="col-lg-6 col-md-6 col-sm-12 col-xs-12"
          v-if="clientSelected != ''"
        >
          <q-card class="my-card">
            <q-table
              bordered
              auto-width
              title="Datos del Cliente"
              :rows="formattedRowsClient"
              :columns="columnsClient"
              row-key="id"
              hide-bottom
              color="primary"
              card-class="bg-grey-6 text-white "
              table-class="text-grey-10"
              table-header-class="text-white"
              class="my-table"
              dense
            >
            </q-table>
          </q-card>
        </div>
        <div
          class="col-lg-6 col-md-6 col-sm-12 col-xs-12"
          v-if="vehicleSelected != ''"
        >
          <q-card class="my-card">
            <q-table
              dense
              bordered
              auto-width
              title="Datos del vehiculo"
              :rows="formattedRowsVehicle"
              :columns="columnsVehicle"
              row-key="id"
              hide-bottom
              color="primary"
              card-class="bg-grey-6 text-white "
              table-class="text-grey-10"
              table-header-class="text-white"
            />
          </q-card>
        </div>
      </div>
    </q-card-section>
    <!--  -->
    <q-card-section class="col-7 flex flex-center" v-if="vehicleSelected != ''">
      <q-table
        flat
        bordered
        title="Total a pagar"
        :rows="clientSelected"
        :columns="columnsPayment"
        row-key="id"
        :loading="loading"
      >
      </q-table>
    </q-card-section>
  </q-card>
  <q-btn icon="delete" @click="mergeSelectedData()" />
</template>
<script setup>
import { ref, onBeforeMount, defineModel, computed } from "vue";
import axios from "axios";
const clientSelected = defineModel("clientSelected");
const vehicleSelected = defineModel("vehicleSelected");
const selectedTableWorks = defineModel("selectedTableWorks");
const VehicleParts = defineModel("VehicleParts");
let listCobro = [];
const cobros = ref({
  cant: 0,
  name: "",
  unitePrice: 0,
  subTotal: 0,
});
const columnsClient = [
  {
    name: "name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "lastName",
    align: "center",
    label: "Apellido",
    field: "lastName",
    sortable: true,
  },
  {
    name: "nit",
    align: "center",
    label: "Nit",
    field: "nit",
    sortable: true,
  },
  {
    name: "type",
    align: "center",
    label: "Tipo de cliente",
    field: (row) => row.typeClient.type,

    sortable: true,
  },
];

const columnsVehicle = [
  {
    name: "Placa",
    align: "center",
    label: "Placa",
    field: "placa",
    sortable: true,
  },

  {
    name: "type",
    required: true,
    label: "Tipo",
    align: "center",
    field: (row) => row.vehicleLinea.type,
    sortable: true,
  },
  {
    name: "Linea",
    required: true,
    label: "Linea",
    align: "center",
    field: (row) => row.vehicleLinea.line,
    sortable: true,
  },
  {
    name: "color",
    required: true,
    label: "Color",
    align: "center",
    field: (row) => row.vehicleLinea.color,
    sortable: true,
  },
];

const columnsPayment = [
  {
    name: "Cantidad",
    align: "center",
    label: "Cantidad",
    field: "placa",
    sortable: true,
  },

  {
    name: "name",
    required: true,
    label: "Tipo",
    align: "center",
    field: "name",
    sortable: true,
  },
  {
    name: "Precio unitario",
    required: true,
    label: "Linea",
    align: "center",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "Precio total",
    required: true,
    label: "Color",
    align: "center",
    field: "total",
    sortable: true,
  },
];

const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.name.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.stock
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase()) ||
      row.unitPrice
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase())
  );
});

const formattedRowsClient = computed(() => {
  // console.log(JSON.stringify(clientSelected.value));
  return clientSelected.value ? [clientSelected.value] : [];
});

const formattedRowsVehicle = computed(() => {
  return vehicleSelected.value ? [vehicleSelected.value] : [];
});

const formattedRowsCobros = computed(() => {
  console.log(JSON.stringify(VehicleParts.value));

  VehicleParts.forEach((vp) => {
    if (vp.detalleVehicleParts != null) {
      listCobro.push({
        name: vp.name,
        cant: vp.detalleVehicleParts,
        unitPrice: vp.unitPrice,
        subTotal: vp.detalleVehicleParts * vp.unitPrice,
      });
    }
  });
  console.log(JSON.stringify(listCobro.value));

  return vehicleSelected.value ? [vehicleSelected.value] : [];
});
function mergeSelectedData() {
  // mergeSelectedData = computed(() => {
  //   const mergedData = [...VehicleParts, ...selectedTableWorks];

  //   // // Agrega los datos de selectedTableWorks a la variable mergedData
  //   // VehicleParts.value.forEach((item) => {
  //   //   mergedData.push(item);
  //   // });

  //   // // Agrega los datos de selectedTableWorks a la variable mergedData
  //   // selectedTableWorks.value.forEach((item) => {
  //   //   mergedData.push(item);
  //   // });
  //   // console.log(JSON.stringify(mergedData));
  //   console.log(JSON.stringify(mergedData));

  //   return mergedData;
  // });
  console.log("parts: " + JSON.stringify(VehicleParts));
  console.log(">" + JSON.stringify(selectedTableWorks));
}
</script>
