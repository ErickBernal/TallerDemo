<template>
  <q-card>
    <q-card-section
      :class="editMode ? 'bg-teal text-white' : 'bg-grey-8 text-white'"
    >
      <div :class="editMode ? 'text-h6 text-right' : 'text-h6 text-left'">
        {{ editMode ? "Editar repuesto" : "Crear Repuesto" }}
      </div>
    </q-card-section>
    <!--  -->
    <q-card-section horizontal>
      <q-card-section class="q-pt-xs col-7">
        <div class="q-gutter-md">
          <q-form>
            <q-input
              v-model="editedRow.name"
              label="Nombre repuesto"
              type="string"
              mask="string"
              filled
              lazy-rules
              :rules="[(val) => (val && val.length > 0) || 'Ingrese un nombre']"
            />
            <q-input
              v-model="editedRow.stock"
              label="Cantidad"
              type="number"
              mask="int"
              filled
              lazy-rules
              :rules="[
                (val) => (val && val.length > 0) || 'Ingrese una cantidad',
              ]"
            />
            <q-input
              v-model="editedRow.unitPrice"
              label="Q. (precio unitario)"
              type="number"
              mask="int"
              filled
              lazy-rules
              :rules="[
                (val) => (val && val.length > 0) || 'Ingrese un precio valido',
              ]"
            />
            Seleccione la Compatibilidad del repuesto.
            <q-select
              filled
              v-model="multipleSelectedVehicleLInes"
              multiple
              :options="vehicleLines"
              :option-value="'id'"
              :option-label="optionLabelTemplate"
              label="Compatibilidad"
              style="width: 250px"
            />
          </q-form>
        </div>
      </q-card-section>

      <!--  -->
      <q-card-section class="col-5 flex flex-center">
        <q-uploader
          style="max-width: 300px"
          label="Seleccione una imagen"
          multiple
          accept=".jpg, image/*"
        ></q-uploader>
      </q-card-section>

      <!--  -->
    </q-card-section>
    <!-- Botones  -->
    <q-card-section v-if="editMode">
      <q-btn
        class="bg-teal text-white text-left"
        label="Actualizar repuesto"
        color="primary"
        @click="updateVehiclePart"
      />
      |
      <q-btn
        class="text-white"
        label="cancelBtnar"
        color="red"
        @click="cancelBtn"
      />
    </q-card-section>
    <q-card-section v-else>
      <q-btn
        class="bg-grey-8 text-white"
        label="Crear repuesto"
        color="primary"
        @click="saveVehiclePart"
      />
    </q-card-section>
    <!--  -->
  </q-card>
</template>

<script setup>
import { ref, onMounted, defineModel } from "vue";
import axios from "axios";
import { useQuasar } from "quasar";

//props using v-model
const editMode = defineModel("editMode");
const editedRow = defineModel("editedRow");
//
const { localStorage, notify } = useQuasar();
const isActiveNewVehicle = ref(true);
const vehicleLines = ref([]);
const multipleSelectedVehicleLInes = ref([]);

const optionLabelTemplate = (option) => {
  return `${option.type} ${option.vehicleBrand.brand} ${option.line} ${option.vehicleModel.model}`;
};

async function saveVehiclePart() {
  if (multipleSelectedVehicleLInes.value == "") {
    alert("Debe seleccionar compatibilidad para la herramienta.");
    return;
  }

  if (
    !(
      editedRow.value.name == "" ||
      editedRow.value.stock == "" ||
      editedRow.value.unitPrice == ""
    )
  ) {
    try {
      const response = await axios.post(
        process.env.API_BASE_URL + "/taller/VehicleParts",
        {
          name: editedRow.value.name,
          stock: editedRow.value.stock,
          unitPrice: editedRow.value.unitPrice,
        }
      );
      alert("Se agrego el nuevo repuesto: " + editedRow.value.name);
      editedRow.value.name = "";
      editedRow.value.stock = 0;
      editedRow.value.unitPrice = 0;
      createVehiclePartCompatible(response.data);
    } catch (error) {
      console.error("Error al agregar Repuesto:", error);
    }
    isActiveNewVehicle.value = !isActiveNewVehicle.value;
  } else alert("Complete todos los campos");
}
//-------------
async function createVehiclePartCompatible(vehiclePart) {
  //iterando la lista de vehiculos compatibles.
  for (const line of multipleSelectedVehicleLInes.value) {
    try {
      const response = await axios.post(
        process.env.API_BASE_URL + "/taller/VPCompatible",
        {
          vehicleLineaId: line.id,
          vehiclePartId: vehiclePart.id,
        }
      );
    } catch (error) {
      console.error("Error al agregar repuesto compatible:", error);
    }
  }
}
//-------------
async function updateVehiclePart() {
  try {
    const url = process.env.API_BASE_URL + "/taller/VehicleParts";
    await axios.post(url, {
      id: editedRow.value.id,
      name: editedRow.value.name,
      stock: editedRow.value.stock,
      unitPrice: editedRow.value.unitPrice,
    });
    alert(
      "Se actualizo el campo: <" + editedRow.value.name + "> correctamente."
    );

    // luego con el response, realizar la carga de la talba
    // repuesto compatible
    editedRow.value.name = "";
    editedRow.value.stock = 0;
    editedRow.value.unitPrice = 0;
    editMode.value = false;
  } catch (error) {
    console.error("Error al guardar los cambios:", error);
  }
}

async function getVehicleLines() {
  try {
    const url = process.env.API_BASE_URL + "/taller/VehicleLine";
    const response = await axios.get(url);
    vehicleLines.value = response.data;
  } catch (error) {
    console.error("Error al obtener datos:", error);
  }
}

function cancelBtn() {
  editMode.value = false;
  editedRow.value.name = "";
  editedRow.value.stock = 0;
  editedRow.value.unitPrice = 0;
}

function start() {
  localStorage.set("owner", false);
  getVehicleLines();
}

onMounted(start);
</script>
