<template>
  <!-- <q-page class="q-pa-md"> -->
  <q-card class="my-card" flat bordered shadow-xl>
    <!--  -->
    <q-card-section class="bg-teal text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Crear nuevo usuario</div>
    </q-card-section>
    <q-card-section>
      <br />
      <q-form class="q-gutter-md">
        <q-input
          v-model="newClient.name"
          label="Nombre"
          filled
          type="string"
          mask="string"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un Nombre']"
        />
        <q-input
          v-model="newClient.lastName"
          label="Apellido"
          filled
          type="string"
          mask="string"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un Apellido']"
        />
        <q-input
          v-model="newClient.dpi"
          label="DPI"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un DPI valido']"
        />
        <q-input
          v-model="newClient.nit"
          label="NIT"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[(val) => (val && val.length > 0) || 'Ingrese un NIT valido']"
        />
        <q-input
          v-model="newClient.phone"
          label="Teléfono"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[
            (val) =>
              (val && val.length == 8) ||
              'Ingrese un numero de telefono valido',
          ]"
        />
        <q-input
          v-model="newClient.cellphone"
          label="Celular"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[
            (val) =>
              (val && val.length == 8) ||
              'Ingrese un numero de telefono valido',
          ]"
        />

        <q-select
          v-model="newClient.TypeClientId"
          :options="clientTypes"
          label="Tipo de Cliente"
          filled
        />
        <!--  -->
        <br />
        <AddressComponent
          v-model:selectedMunicipality="selectedMunicipality"
          v-model:selectedAddress="selectedAddress"
          v-model:selectedZone="selectedZone"
        />
        <!--  -->
        <q-btn type="submit" @click="createNewClient" color="primary"
          >Crear nuevo usuairo</q-btn
        >
      </q-form>
    </q-card-section>
  </q-card>
  <!-- </q-page> -->
</template>

<script setup>
import { ref, onMounted, onBeforeMount, defineProps } from "vue";
import axios from "axios";
import AddressComponent from "./AddressComponent.vue";
// import { useQuasar } from "quasar";
// const { cookies, fullscreen } = useQuasar();
const newClient = ref({
  MunicipalityId: "",
  Zone: "",
  name: "",
  lastName: "",
  dpi: "",
  nit: "",
  phone: "",
  cellphone: "",
  TypeClientId: "",
});
const clientTypes = ref([]);
const countries = ref([]);
const departments = ref([]);
const municipalities = ref([]);
const selectedCountry = ref("");
const selectedDepartment = ref("");
// using v-model to senda dta child <->parent
const selectedMunicipality = ref("");
const selectedAddress = ref("");
const selectedZone = ref("");
//child to RudClient.vue
const createDialogVisible = defineModel("createDialogVisible");

onBeforeMount(async () => {
  getTypeClients();
  getCountries();
});

async function getTypeClients() {
  // Obtener tipos de cliente
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/TypeClient"
    );
    // Mapear la respuesta para obtener los tipos de cliente
    clientTypes.value = response.data.map((type) => ({
      label: type.type,
      id: type.id,
    }));
  } catch (error) {
    console.error("Error al obtener tipos de cliente:", error);
    alert("Error al obtener tipos de cliente:");
  }
}

async function getCountries() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Country"
    );
    countries.value = response.data.map((res) => ({
      label: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener países:", error);
    alert("Error al obtener paises");
  }
}

const createNewClient = async () => {
  if (newClient.value.name == "") {
    alert("Agregue un nombre");
  } else if (newClient.value.lastName == "") {
    alert("Agregue un apellido");
  } else if (newClient.value.dpi == "") {
    alert("Agregue DPI");
  } else if (newClient.value.nit == "") {
    alert("Agregue nit");
  } else if (newClient.value.phone == "") {
    alert("Agregue telefono");
  } else if (newClient.value.cellphone == "") {
    alert("Agregue celular");
  } else if (newClient.value.TypeClientId.id == "") {
    alert("Seleccione un tipo de cliente");
  } else if (newClient.value.zone == "") {
    alert("Agregue una zona valida");
  } else if (newClient.value.address == "") {
    alert("Agregue la direccion");
  } else if (selectedMunicipality.value.id == "") {
    alert("Seleccione una municipalidad");
  } else {
    try {
      const response = await axios.post(
        process.env.API_BASE_URL + "/taller/Client",
        {
          name: newClient.value.name,
          lastname: newClient.value.lastName,
          dpi: newClient.value.dpi,
          nit: newClient.value.nit,
          phone: newClient.value.phone,
          cellphone: newClient.value.cellphone,
          typeClientId: newClient.value.TypeClientId.id,
          zone: selectedZone.value,
          address: selectedAddress.value,
          municipalityId: selectedMunicipality.value.id,
        }
      );
      console.log("Address agregado correctamente:", response.data);
      alert("Address agregado correctamente:");
    } catch (error) {
      alert("Error al agregar cliente:");
      console.error("Error al agregar cliente:", error);
    }
    createDialogVisible.value = false;
  }
};
</script>

<style>
.my-card {
  min-width: 500px;
  width: auto;
}
</style>
