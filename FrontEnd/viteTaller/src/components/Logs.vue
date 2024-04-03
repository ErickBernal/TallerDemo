<template>
  <div>
    <div>
      <NavBar />
    </div>
    <div>
      <h2>Reporte de trasacciones</h2>
      <input type="text" v-model="searchTerm" placeholder="Buscar Log..." />
      <br />
      <br />
      <table class="Logs-table">
        <thead>
          <tr style="color: black">
            <th>Id</th>
            <th>Descripcion</th>
            <th>Fecha</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="Log in filteredLogs" :key="Log.id">
            <td>{{ Log.id }}</td>
            <td>{{ Log.description }}</td>
            <td>{{ Log.date }}</td>
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
      Logs: [],
      searchTerm: "",
    };
  },
  computed: {
    filteredLogs() {
      return this.Logs;
    },
  },
  mounted() {
    this.fetchLogs();
  },
  methods: {
    async fetchLogs() {
      try {
        const response = await fetch("https://localhost:7099/taller/Logs");
        const data = await response.json();
        this.Logs = data;
      } catch (error) {
        alert("Error al obtener Logs:" + error);
        // console.error("Error al obtener Logs:", ºerror);
      }
    },
  },
};
</script>

<style scoped>
.Logs-table {
  width: 100%;
  border-collapse: collapse;
}

.Logs-table th,
.Logs-table td {
  border: 1px solid #dddddd;
  text-align: left;
  padding: 8px;
}

.Logs-table th {
  background-color: #f2f2f2;
}
</style>
