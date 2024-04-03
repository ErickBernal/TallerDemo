<template>
  <div>
    <div>
      <NavBar />
    </div>
    <div>
      <h2>Listado de Repuestos de Vehículos</h2>
      <ul>
        <li v-for="repuesto in repuestos" :key="repuesto.id">
          {{ repuesto.nombre_repuesto }} - Cantidad: {{ repuesto.cantidad }}
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
import NavBar from "./NavBar.vue";
export default {
  components: {
    NavBar,
  },
  data() {
    return {
      repuestos: [],
    };
  },
  mounted() {
    // Aquí llamamos a la función para obtener los repuestos de vehículos
    this.fetchRepuestos();
  },
  methods: {
    async fetchRepuestos() {
      try {
        // Realizamos la solicitud GET para obtener los repuestos de vehículos
        const response = await fetch("URL_DE_TU_API");
        const data = await response.json();
        this.repuestos = data; // Actualizamos el arreglo de repuestos con los datos obtenidos
      } catch (error) {
        console.error("Error al obtener los repuestos:", error);
      }
    },
  },
};
</script>

<style scoped>
/* Agrega estilos si es necesario */
</style>
