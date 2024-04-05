<template>
  <q-page class="q-pa-md">
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
  </q-page>
</template>

<script setup>
import { ref, onMounted, computed } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

const $q = useQuasar();
const loading = ref(false);
const rows = ref([]);
const columns = [
  {
    name: "description",
    align: "left",
    label: "Description",
    field: "description",
    sortable: true,
  },
  {
    name: "date",
    align: "left",
    label: "Date",
    field: "date",
    sortable: true,
  },
];

const filterText = ref("");
const filteredRows = computed(() => {
  return rows.value.filter(
    (row) =>
      row.description.toLowerCase().includes(filterText.value.toLowerCase()) ||
      row.date.toString().toLowerCase().includes(filterText.value.toLowerCase())
  );
});

async function fetchData() {
  loading.value = true;
  try {
    const url = process.env.API_BASE_URL + "/taller/Logs";
    const response = await axios.get(url);
    rows.value = response.data.map((res) => ({
      id: res.id,
      description: res.description,
      date: res.date,
    }));
    // console.log("--1123123123123123 " + rows.value[0].description);
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

onMounted(start);
</script>
