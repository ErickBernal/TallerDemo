<template>
  <q-card>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Servicios implementados</div>
    </q-card-section>
    <!--  -->
    <q-card-section>
      <q-table
        flat
        bordered
        title="Trabajos"
        :rows="serviceWork"
        :columns="columnsWorks"
        row-key="id"
        :loading="loadingWorks"
        selection="multiple"
        v-model:selected="selectedTableWorks"
      ></q-table>
    </q-card-section>
    <!--  -->
  </q-card>
</template>

<script setup>
import { ref, onBeforeMount, defineModel } from "vue";
import axios from "axios";

const serviceWork = ref([]);
const selectedTableWorks = defineModel("selectedTableWorks");
const loadingWorks = ref(false);
const columnsWorks = [
  {
    name: "work",
    align: "center",
    label: "Nombre",
    field: "work",
    sortable: true,
  },
  {
    name: "Precio",
    align: "center",
    label: "(Q) Precio del trabajo",
    field: "priceWork",
    sortable: true,
  },
];

async function getServicesWork() {
  loadingWorks.value = true;
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/ServiceWork"
    );
    serviceWork.value = response.data;
  } catch (error) {
    alert("Error al obtener ServiceWork");
  }
  loadingWorks.value = false;
}

onBeforeMount(() => {
  getServicesWork();
});
</script>
