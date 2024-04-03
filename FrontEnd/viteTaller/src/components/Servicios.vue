<template>
  <div>
    <div>
      <NavBar />
    </div>
    <div class="user-selection">
      <h1>Servicios</h1>
      <br />
      <div class="user-buttons">
        <button v-on:click="toNuevoServicio">Nuevo</button>
        <router-link :to="'/estadoServicio'">
          <button>Estado</button>
        </router-link>
      </div>
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
    return {};
  },
  methods: {
    toNuevoServicio() {
      this.$router.push("/nuevoServicio");
    },
  },
};
</script>

<style scoped>
.user-selection {
  text-align: center;
}

.user-buttons {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.user-buttons button {
  width: 200px;
  height: 200px;
  margin: 10px;
  font-size: 24px;
  border: none;
  border-radius: 0;
  background-color: #0815ce;
  color: white;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.user-buttons button:hover {
  background-color: #0815ce;
}

@media screen and (max-width: 768px) {
  .user-buttons button {
    width: 150px;
    height: 150px;
    font-size: 20px;
  }
}
</style>
