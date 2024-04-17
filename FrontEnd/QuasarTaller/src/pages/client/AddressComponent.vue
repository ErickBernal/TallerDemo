<template>
  <q-card class="q-mb-md">
    <q-card-section class="bg-grey-8 text-white">
      <div class="text-h5 q-mt-sm q-mb-xs">Direccion</div>
    </q-card-section>
    <q-card-section>
      <br />
      <q-form class="q-gutter-md">
        <q-select
          v-model="selectedCountry"
          :options="countries"
          :option-value="'id'"
          :option-label="'name'"
          label="País"
          @update:modelValue="updateSelectedCountryId"
          filled
        />
        <q-select
          v-model="selectedDepartment"
          :options="departments"
          :option-value="'id'"
          :option-label="'name'"
          label="Departamento"
          @update:modelValue="updateSelectedDepartmentId"
          filled
        />
        <q-select
          v-model="selectedMunicipality"
          :options="municipalities"
          :option-value="'id'"
          :option-label="'name'"
          label="Municipio"
          filled
        />
        <q-input
          v-model="selectedAddress"
          label="Direccion"
          filled
          type="string"
          mask="string"
          lazy-rules
          :rules="[
            (val) => (val && val.length > 0) || 'Ingrese una direccion valida',
          ]"
        />
        <q-input
          v-model="selectedZone"
          label="Zona"
          filled
          type="number"
          mask="int"
          lazy-rules
          :rules="[
            (val) => (val && val.length > 0) || 'Ingrese una zona valida',
          ]"
        />
      </q-form>
    </q-card-section>
  </q-card>
  <!-- </q-page> -->
</template>

<script setup>
import { ref, onMounted, onBeforeMount, defineProps, defineModel } from "vue";
import axios from "axios";
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
  zone: "",
  address: "",
  MunicipalityId: "",
});

const varUpdateClient = ref([]);
const clientTypes = ref([]);
const countries = ref([]);
const departments = ref([]);
const municipalities = ref([]);
//new props
const selectedCountry = defineModel("selectedCountry");
const selectedDepartment = defineModel("selectedDepartment");
const selectedMunicipality = defineModel("selectedMunicipality");
const selectedAddress = defineModel("selectedAddress");
const selectedZone = defineModel("selectedZone");

onBeforeMount(async () => {
  getCountries();
});

async function getCountries() {
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Country"
    );
    countries.value = response.data.map((res) => ({
      name: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener países:", error);
    alert("Error al obtener paises");
  }
}

// Método para actualizar el ID del Departamento seleccionado
const updateSelectedDepartmentId = async (value) => {
  selectedMunicipality.value = "";
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Municipalities/" + value.id
    );
    municipalities.value = response.data.map((res) => ({
      name: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
};

// Método para actualizar el ID del país seleccionado
const updateSelectedCountryId = async (value) => {
  selectedDepartment.value = "";
  selectedMunicipality.value = "";
  // Obtener lista de departamentos
  try {
    const response = await axios.get(
      process.env.API_BASE_URL + "/taller/Department/" + value.id
    );
    departments.value = response.data.map((res) => ({
      name: res.name,
      id: res.id,
    }));
  } catch (error) {
    console.error("Error al obtener departamentos:", error);
  }
};
</script>
