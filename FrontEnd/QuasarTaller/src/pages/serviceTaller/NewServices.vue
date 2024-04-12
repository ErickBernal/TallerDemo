<template>
  <!-- <q-page> -->
  <!-- <div class="row q-col-gutter-sm q-ma-xs">
      <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12"> -->
  <q-card>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Nuevo Servicio</div>
    </q-card-section>
    <!--  -->
    <q-card class="q-mb-md">
      <q-card-section>
        <q-form @submit="submitForm" class="q-gutter-md">
          <q-input
            v-model="newServices.km"
            label="Kilometraje del vehiculo"
            type="number"
            mask="int"
            filled
            lazy-rules
            :rules="[
              (val) =>
                (val && val.length > 0) || 'Ingrese un kilometraje valido',
            ]"
          />
          <q-select
            v-model="newServices.servicesType"
            :options="servicesType"
            label="Tipo de servicio"
            filled
          />
          <q-select
            v-model="newServices.vehicle"
            :options="vehicles"
            label="Vehiculo"
            @update:modelValue="getVehicleLineById"
            filled
          >
            <template v-slot:append>
              <span v-if="vehicleLine">
                {{ vehicleLine.line }} {{ vehicleLine.type }}
                {{ vehicleLine.color }}
              </span>
              <span v-else> Cargando... </span>
            </template>
          </q-select>
          <!--  -->
          <q-card class="my-card">
            <!--  -->
            <q-card-section class="bg-grey-8 text-white">
              <div class="text-h8">Asignar Cliente</div>
            </q-card-section>
            <!--  -->
            <q-card class="q-mb-md">
              <q-card class="q-mb-md">
                <q-card-section>
                  <div class="q-gutter-md">
                    <div class="q-gutter-md q-mb-md">
                      <q-input
                        v-model="filterText"
                        placeholder="Buscar Cliente..."
                      />
                      <!-- <q-btn @click="filterData" label="Filtrar" color="primary" /> -->
                    </div>
                    <q-table
                      :rows="filteredRows"
                      :columns="columns"
                      row-key="id"
                      :loading="loading"
                      selection="single"
                      v-model:selected="rowSelected"
                      no-data-label="No hay datos disponibles"
                    />
                    selected: {{ JSON.stringify(rowSelected) }}
                  </div>
                </q-card-section>
              </q-card>
            </q-card>
            <!--  -->
          </q-card>
          <!--  -->
          <q-btn
            type="submit"
            label="Crear servicio"
            class="bg-teal text-white"
          />
        </q-form>
      </q-card-section>
    </q-card>
    <!--  -->
  </q-card>
  <!-- </div>
    </div> -->
  <!-- </q-page> -->
</template>
<!-- ------------------------------------------------------------------------------ -->
<script setup>
import { ref, onMounted, computed } from "vue";

import axios from "axios";

const newServices = ref({
  vehicle: "",
  servicesType: "",
  km: "",
});

const rowSelected = ref([]);
const vehicles = ref([]);
const servicesType = ref([]);
const vehicleLine = ref([]); // Inicialmente establecido como null
const loading = ref(false);
const rows = ref([]);
const columns = [
  {
    name: "Name",
    align: "left",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "lastName",
    align: "left",
    label: "Apellido",
    field: "lastName",
    sortable: true,
  },
  {
    name: "DPI",
    align: "left",
    label: "DPI",
    field: "dpi",
    sortable: true,
  },
];

const filterText = ref("");
const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.name.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.lastName
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase()) ||
      row.dpi.toString().toLowerCase().includes(filterText.value.toLowerCase())
  );
});

async function getVehicleLineById(value) {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL +
        "/taller/VehicleLine/" +
        newServices.value.vehicle.vehicleLineaId
    );
    const res = response.data;
    vehicleLine.value = {
      color: res.color,
      type: res.type,
      line: res.line,
    };
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
}

async function getVehicles() {
  try {
    const vehiclesResponse = await axios.get(
      process.env.API_BASE_URL + "/taller/Vehicle"
    );
    vehicles.value = vehiclesResponse.data.map((v) => ({
      label: v.placa,
      placa: v.placa,
      id: v.id,
      vehicleLineaId: v.vehicleLineaId,
    }));
  } catch (error) {
    alert("Error al obtener vehiculos:");
  }
}

async function getServicesType() {
  try {
    const servicesResponse = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceType"
    );
    servicesType.value = servicesResponse.data.map((s) => ({
      label: s.type,
      id: s.id,
      type: s.type,
      serviceListWorks: s.serviceListWorks,
      serviceDetalles: s.serviceDetalles,
    }));
  } catch (error) {
    console.error("Error al obtener tipos de servicios:", error);
  }
}

async function submitForm() {
  if (rowSelected.value.length > 0) {
    // console.log(
    //   "--5555555555555> " + JSON.stringify(rowSelected.value[0].name)
    // );

    try {
      const sendData = {
        state: 1,
        km: newServices.value.km,
        serviceTypeId: newServices.value.servicesType.id,
        vehicleId: newServices.value.vehicle.id,
        clientId: rowSelected.value[0].id,
      };
      const response = await axios.post(
        process.env.API_BASE_URL + "/taller/ServiceDetalle",
        sendData
      );

      alert("Servicio agregado correctamente:");
    } catch (error) {
      alert("SError al agregar nuevo servicio");
    }
  } else {
    alert("Asigne un cliente");
  }
}

async function getVehicleParts() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/Client";
    const response = await axios.get(url);
    rows.value = response.data.map((res) => ({
      label: res.name,
      id: res.id,
      name: res.name,
      lastName: res.lastName,
      dpi: res.dpi,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

onMounted(async () => {
  getVehicles();
  getServicesType();
  getVehicleParts();
});
</script>
