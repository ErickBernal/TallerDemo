<template>
  <div class="q-pa-md row items-start q-gutter-md">
    <!--  -->
    <!--  -->

    <q-dialog v-model="isActiveFindVehicle">
      <q-card class="my-card">
        <!--  -->
        <q-card-section class="bg-grey-8 text-white">
          <div class="text-h6">Buscar repuesto</div>
        </q-card-section>
        <!--  -->
        <q-card class="q-mb-md">
          <q-card class="q-mb-md">
            <q-card-section>
              <div class="q-gutter-md">
                <div class="q-gutter-md q-mb-md">
                  <q-input v-model="filterText" placeholder="Buscar..." />
                  <!-- <q-btn @click="filterData" label="Filtrar" color="primary" /> -->
                </div>
                <q-table
                  :rows="filteredRows"
                  :columns="columns"
                  row-key="id"
                  :loading="loading"
                  no-data-label="No hay datos disponibles"
                />
              </div>
            </q-card-section>
          </q-card>
        </q-card>
        <!--  -->
      </q-card>
    </q-dialog>
    <!--  -->
  </div>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const { localStorage, notify } = useQuasar();
const loading = ref(false);
const rows = ref([]);
const isActiveFindVehicle = ref(true);

const newVehiclePat = ref({
  name: "",
  stock: "",
  unitPrice: "",
});
const columns = [
  {
    name: "Name",
    align: "left",
    label: "Nombre",
    field: "name",
    sortable: true,
  },
  {
    name: "Stock",
    align: "left",
    label: "Stock",
    field: "stock",
    sortable: true,
  },
  {
    name: "precio",
    align: "left",
    label: "Precio Unitario",
    field: "unitPrice",
    sortable: true,
  },
];

const filterText = ref("");
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

async function getVehicleParts() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/VehicleParts";
    const response = await axios.get(url);
    rows.value = response.data.map((res) => ({
      id: res.id,
      name: res.name,
      stock: res.stock,
      unitPrice: res.unitPrice,
    }));
  } catch (error) {
    console.error("Error al obtener datos:", error);
  } finally {
    loading.value = false;
  }
}

function cardShowVehicleParts() {
  isActiveFindVehicle.value = !isActiveFindVehicle.value;
}

function start() {
  getVehicleParts();
  localStorage.set("owner", false);
}

onMounted(start);
</script>
