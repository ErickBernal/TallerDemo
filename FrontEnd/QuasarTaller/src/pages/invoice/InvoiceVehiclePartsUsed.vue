<template>
  <q-card class="my-card" flat bordered shadow-xl>
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Repuestos utilizados</div>
    </q-card-section>
    <q-card-section>
      <q-table
        flat
        bordered
        title="Repuestos"
        :rows="VehicleParts"
        :columns="columnsVehicleParts"
        row-key="id"
        :loading="loading"
      >
        <template #body-cell-Cant="props">
          <q-td :props="props">
            <q-input
              v-model="props.row.detalleVehicleParts"
              label="Cantidad"
              filled
              type="number"
              mask="int"
              lazy-rules
              :rules="[(val) => (val && val.length > 0) || 'Ingrese cantidad']"
            />
          </q-td>
        </template>
      </q-table>
      <q-btn icon="delete" @click="prueba()" />
    </q-card-section>
  </q-card>
</template>
<!-- ---------------------------------------------------------------- -->
<script setup>
import { ref, onBeforeMount, defineModel } from "vue";
import axios from "axios";

const VehicleParts = defineModel("VehicleParts");
const loading = ref(false);
const columnsVehicleParts = [
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
function prueba() {
  console.log("-.-.-->" + JSON.stringify(VehicleParts.value));
  // console.log("----******-->" + JSON.stringify(vehicles.value));
}

async function getVehicleParts() {
  loading.value = true;

  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/VehicleParts"
    );
    VehicleParts.value = response.data;
  } catch (error) {
    alert("Error al obtener Repuesto de vehiculos");
  }
  loading.value = false;
}
onBeforeMount(async () => {
  getVehicleParts();
});
</script>
