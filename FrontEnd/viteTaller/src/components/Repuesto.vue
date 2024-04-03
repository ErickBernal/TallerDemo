<template>
  <div>
    <div>
      <NavBar />
    </div>
    <div>
      <h2>Listado de Repuestos de Vehículos</h2>
      <input
        type="text"
        v-model="searchTerm"
        placeholder="Buscar repuesto..."
      />
      <br />
      <br />
      <table class="repuestos-table">
        <thead>
          <tr style="color: black">
            <th>Id</th>
            <th>Nombre</th>
            <th>Cantidad</th>
            <th>Precio Unitario</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="repuesto in filteredRepuestos" :key="repuesto.id">
            <td>{{ repuesto.id }}</td>
            <td>{{ repuesto.name }}</td>
            <td>{{ repuesto.stock }}</td>
            <td>Q.{{ repuesto.unitPrice }}</td>
            <td>
              <button @click="seleccionarFila(repuesto)">Seleccionar</button>
            </td>
          </tr>
        </tbody>
      </table>
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
      searchTerm: "",
      filaSeleccionada: null,
    };
  },
  computed: {
    filteredRepuestos() {
      return this.repuestos.filter((repuesto) =>
        repuesto.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    },
  },
  mounted() {
    this.fetchRepuestos();
  },
  methods: {
    async fetchRepuestos() {
      try {
        const response = await fetch(
          "https://localhost:7099/taller/VechicleParts"
        );
        const data = await response.json();
        this.repuestos = data;
      } catch (error) {
        console.error("Error al obtener los repuestos:", error);
      }
    },
    async seleccionarFila(repuesto) {
      this.filaSeleccionada = repuesto;
      console.log(this.filaSeleccionada.id);

      // Crear el objeto de datos para la solicitud POST
      const postData = {
        description:
          "Repuestos: Se filtro el repuesto = [id: " +
          repuesto.id +
          " , nombre: " +
          repuesto.name +
          " ]",
        date: new Date().toISOString(),
      };

      // Realizar la solicitud POST
      try {
        const response = await fetch("https://localhost:7099/taller/Logs", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(postData),
        });
        const responseData = await response.json();
        // console.log("Solicitud POST exitosa:", responseData);
      } catch (error) {
        alert("Error al realizar la solicitud POST:" + error);
        // console.error("Error al realizar la solicitud POST:", error);
      }
    },
  },
};
</script>

<style scoped>
.repuestos-table {
  width: 100%;
  border-collapse: collapse;
}

.repuestos-table th,
.repuestos-table td {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

.repuestos-table th {
  background-color: #f2f2f2;
}
</style>
