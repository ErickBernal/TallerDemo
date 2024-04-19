<template>
  <!-- Banner  -->
  <InvoiceTop
    v-model:clientSelected="clientSelected"
    v-model:vehicleSelected="vehicleSelected"
  />
  <div class="row q-col-gutter-sm q-ma-xs">
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <InvoiceVehiclePartsUsed v-model:VehicleParts="VehicleParts" />
    </div>
    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
      <InvoiceImplementationServices
        v-model:selectedTableWorks="selectedTableWorks"
      />
    </div>
  </div>
  <!-- invoice -->
  <br />
  <!-- <InvoicePageArea
    v-model:clientSelected="clientSelected"
    v-model:vehicleSelected="vehicleSelected"
    v-model:selectedTableWorks="selectedTableWorks"
    v-model:VehicleParts="VehicleParts"
  /> -->
  <q-card>
    <q-card-section
      class="bg-teal text-white"
      style="display: flex; justify-content: center; align-items: center"
    >
      <q-btn class="bg-grey-8" label="Facturar" @click="createInvoice" />
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
        :title="formattedInvoiceTittle"
        :rows="mergeInvoiceData"
        :columns="columnsPayment"
        row-key="id"
        :loading="loading"
      >
      </q-table>
    </q-card-section>
  </q-card>
  <q-btn icon="delete" @click="mergeSelectedData()" />
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import { ref, onBeforeMount, computed } from "vue";
import InvoiceTop from "./InvoiceTop.vue";
import InvoiceImplementationServices from "./InvoiceImplementationServices.vue";
import InvoiceVehiclePartsUsed from "./InvoiceVehiclePartsUsed.vue";
import axios from "axios";

const vehicleLine = ref([]);
const clientSelected = ref([]);
const vehicleSelected = ref([]);
const selectedTableRow = ref([]);
const VehiclePartSelected = ref([]);
const serviceWork = ref([]);
const ServiceDetalle = ref([]);
const loading = ref(false);
const selectedTableWorks = ref([]);
const VehicleParts = ref([]);
const invoiceValue = ref([]);

const columnsServicioDetalle = [
  {
    name: "name",
    align: "center",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "stock",
    align: "center",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "unitprice",
    align: "center",
    label: "(Q) Precio unitario",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "Cant",
    align: "center",
    label: "Cant.compra",
    field: "Cant",
    sortable: true,
  },
];

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
    field: "cant",
    sortable: true,
  },

  {
    name: "name",
    required: true,
    label: "Nombre",
    align: "center",
    field: "name",
    sortable: true,
  },
  {
    name: "Precio unitario",
    required: true,
    label: "Precio unitario",
    align: "center",
    field: "unitPrice",
    sortable: true,
  },
  {
    name: "Precio total",
    required: true,
    label: "(Q)sub total",
    align: "center",
    field: "total",
    sortable: true,
  },
];
const formattedRowsClient = computed(() => {
  // console.log(JSON.stringify(clientSelected.value));
  return clientSelected.value ? [clientSelected.value] : [];
});

const formattedRowsVehicle = computed(() => {
  return vehicleSelected.value ? [vehicleSelected.value] : [];
});

function getInvoiceValue() {
  const mergedData = [];
  const payment = ref(0);

  // Agrega los datos de selectedTableWorks a la variable mergedData
  VehicleParts.value.forEach((item) => {
    if (item.detalleVehicleParts != null) {
      mergedData.push({
        cant: item.detalleVehicleParts,
        name: item.name,
        unitPrice: item.unitPrice,
        total: item.unitPrice * item.detalleVehicleParts,
      });
      payment.value = payment.value + item.unitPrice * item.detalleVehicleParts;
    }
  });

  // Agrega los datos de selectedTableWorks a la variable mergedData
  selectedTableWorks.value.forEach((item) => {
    mergedData.push({
      cant: 1,
      name: item.work,
      unitPrice: item.priceWork,
      total: item.priceWork,
    });
    payment.value = payment.value + item.priceWork;
  });
  // console.log("merge: " + JSON.stringify(mergedData));
  return payment.value;
}

const formattedInvoiceTittle = computed(() => {
  const payment = ref(0);
  payment.value = getInvoiceValue();
  return payment.value != 0
    ? "Total a pagar: Q " + payment.value
    : "Total a pagar: Q 0";
});

async function getServiceDetalle() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceDetalle"
    );
    ServiceDetalle.value = response.data;
  } catch (error) {
    alert("Error al obtener ServiceWork");
  }
}

const mergeInvoiceData = computed(() => {
  const mergedData = [];
  // Agrega los datos de selectedTableWorks a la variable mergedData
  VehicleParts.value.forEach((item) => {
    if (item.detalleVehicleParts != null) {
      mergedData.push({
        cant: item.detalleVehicleParts,
        name: item.name,
        unitPrice: item.unitPrice,
        total: item.unitPrice * item.detalleVehicleParts,
      });
    }
  });

  // Agrega los datos de selectedTableWorks a la variable mergedData
  selectedTableWorks.value.forEach((item) => {
    mergedData.push({
      cant: 1,
      name: item.work,
      unitPrice: item.priceWork,
      total: item.priceWork,
    });
  });
  // console.log("merge: " + JSON.stringify(mergedData));
  return mergedData;
});

async function createInvoice() {
  loadInvoice();
  loadDetalleVehicleParts();
}

async function loadInvoice() {
  try {
    const sendData = {
      dateTime: new Date(),
      value: getInvoiceValue(),
    };
    const response = await axios.post(
      process.env.API_BASE_URL + "/taller/Invoice",
      sendData
    );
    alert("Factura creada correctamente:");
    invoiceValue.value = response.data;
  } catch (error) {
    alert("Error al crear la factura");
  }
}

async function loadDetalleVehicleParts() {
  try {
    const sendData = {
      vehiclePartId: new Date(),
      serviceDetalleId: getInvoiceValue(),
      invoiceId: invoiceValue.value.id,
    };
    const response = await axios.post(
      process.env.API_BASE_URL + "/taller/DetalleVP",
      sendData
    );
    alert("Factura creada correctamente:");
    invoiceValue.value = response.data;
  } catch (error) {
    alert("Error al crear la factura");
  }
}

onBeforeMount(async () => {
  getServiceDetalle();
});
</script>

<style>
.horizontal-card {
  display: flex;
  flex-direction: row;
  justify-content: center; /* Para centrar los elementos horizontalmente */
}

.horizontal-section {
  display: flex;
  flex-direction: row;
  justify-content: space-between; /* Para distribuir los elementos horizontalmente */
  width: 100%; /* Opcional, para asegurarse de que los elementos ocupen todo el ancho disponible */
}
.w-50 {
  width: 50%;
}

.w-100 {
  width: 100%;
}
.h-50 {
  height: 30%;
}

.h-100 {
  height: 70%;
}
.white-label .q-field__label {
  color: white;
}
</style>
