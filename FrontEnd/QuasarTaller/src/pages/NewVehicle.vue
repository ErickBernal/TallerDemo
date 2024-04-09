<template>
  <h5>Nuevo vehiculo</h5>
  <q-page class="q-pa-md">
    <div class="q-gutter-md">
      <div class="q-gutter-md q-mb-md">
        Buscar linea
        <q-input v-model="filterText" placeholder="Buscar..." />
        <!-- <q-btn @click="filterData" label="Filtrar" color="primary" /> -->
      </div>
      <q-table
        :rows="filteredRows"
        :columns="columns"
        row-key="id"
        :loading="loading"
        no-data-label="No hay datos disponibles"
      >
        <template v-slot:body-cell-Selected="props">
          <q-td :props="props">
            <q-checkbox
              v-model="props.row.selected"
              @change="logSelectedRow(props.row)"
            />
          </q-td>
        </template>
      </q-table>
    </div>
    <br />
    <br />
    <q-form>
      <q-input v-model="newVehicle.Placa" label="Ingrese No. Placa" />
    </q-form>
    <br />
    <q-btn @click="saveNewVehicle" label="Guardar" color="primary" />
  </q-page>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const $q = useQuasar();
const loading = ref(false);
const rows = ref([]);
const selectedRowId = ref(null); // Nueva propiedad para almacenar el ID de la fila seleccionada
const newVehicle = ref({
  Placa: "",
  VehicleLineId: "",
});

const columns = [
  {
    name: "Line",
    align: "left",
    label: "Linea",
    field: "line",
  },
  {
    name: "Color",
    align: "left",
    label: "Color",
    field: "color",
  },
  {
    name: "Type",
    align: "left",
    label: "Tipo",
    field: "type",
  },
  {
    name: "Selected",
    align: "left",
    label: "Seleccionar",
    field: "selected",
  },
];

const filterText = ref("");
const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.type.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.color
        .toString()
        .toLowerCase()
        .includes(filterText.value.toLowerCase()) ||
      row.line.toString().toLowerCase().includes(filterText.value.toLowerCase())
  );
});

async function fetchData() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/VehicleLine";
    const response = await axios.get(url);
    rows.value = response.data.map((res) => ({
      id: res.id,
      color: res.color,
      type: res.type,
      line: res.line,
      selected: false,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

function start() {
  fetchData();
  $q.localStorage.set("owner", false);
}

function logSelectedRow(row) {
  if (row.selected) {
    console.log("Fila seleccionada:", row);
  }
}

function selected() {
  console.log("Información de las filas:");
  rows.value.forEach((row) => {
    console.log("ID:", row.id);
    console.log("Color:", row.color);
    console.log("Tipo:", row.type);
    console.log("Linea:", row.line);
    console.log("Seleccionado:", row.selected);
    console.log("---------------------------");
  });
}
const idSelected = ref("");

const saveNewVehicle = async () => {
  selected();

  rows.value.forEach((row) => {
    if (row.selected) {
      idSelected.value = row.id;
    }
  });
  const dataSend = ref({
    Placa: newVehicle.value.Placa,
    VehicleLineaId: idSelected.value,
  });

  console.log("--> " + dataSend.value.Placa, dataSend.value.VehicleLineaId);
  try {
    const response = await axios.post(
      process.env.API_BASE_URL + "/taller/Vehicle",
      dataSend.value
    );

    console.log("Address agregado correctamente:", response.data);
    // $q.notify({
    //   type: "positive",
    //   message: "Cliente agregado correctamente",
    // });
  } catch (error) {
    console.error("Error al agregar cliente:", error);
    // $q.notify({
    //   type: "negative",
    //   message: "Error al agregar cliente",
    // });
  }
};

onMounted(start);
</script>
