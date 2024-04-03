<template>
  <div>
    <div>
      <NavBar />
    </div>
    <div class="user-selection">
      <h1>Taller</h1>
      <h3>Mecanica en general</h3>
      <!-- <div class="user-buttons">
      <router-link
        v-for="user in users"
        :key="user.id"
        :to="'/repuesto/' + user.id"
      >
        <button>
          {{ user.name }}
        </button>
      </router-link>
    </div> -->
      <br />
      <div class="user-buttons">
        <button v-on:click="toRepuestos">Repuestoss</button>
        <router-link :to="'/servicios'">
          <button>Servicios</button>
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
    return {
      users: [
        { id: 1, name: "Repuestos" },
        { id: 2, name: "Servicios" },
      ],
    };
  },
  methods: {
    toggleVisibility() {
      this.isVisible = !this.isVisible;
    },
    sayHello() {
      alert("Hello!");
    },
    toggleHighlight() {
      this.isHighlighted = !this.isHighlighted;
    },
    toRepuestos() {
      this.$router.push("/repuesto");
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
  background-color: #e50914;
  color: white;
  cursor: pointer;
  transition: background-color 0.3s ease;
}

.user-buttons button:hover {
  background-color: #b2070f;
}

@media screen and (max-width: 768px) {
  .user-buttons button {
    width: 150px;
    height: 150px;
    font-size: 20px;
  }
}
</style>
