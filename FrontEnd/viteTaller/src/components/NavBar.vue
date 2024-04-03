<template>
  <nav class="navbar">
    <div class="navbar-brand">
      <router-link to="/">Inicio</router-link>
    </div>
    <div class="navbar-menu" :class="{ active: isMenuActive }">
      <div class="menu-toggle" @click="toggleMenu">Menú</div>
      <ul v-show="isMenuActive">
        <li>
          <router-link to="/Logs">Logs</router-link>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      components: [{ name: "Logs", route: "Logs" }],
      isMenuActive: false,
    };
  },
  methods: {
    toggleMenu() {
      this.isMenuActive = !this.isMenuActive;
    },
  },
};
</script>

<style scoped>
.navbar {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  background-color: #333;
  color: white;
  padding: 10px 20px;
}

.navbar-brand {
  font-size: 18px;
}

.navbar-menu {
  display: flex;
  align-items: center;
}

.menu-toggle {
  cursor: pointer;
  margin-right: 20px;
}

.navbar-menu ul {
  list-style-type: none;
  padding: 0;
  display: flex;
}

.navbar-menu ul li {
  margin-right: 20px;
}

.navbar-menu ul li:last-child {
  margin-right: 0;
}

.navbar-menu a {
  color: #fff;
  text-decoration: none;
  transition: color 0.3s ease;
}

.navbar-menu a:hover {
  color: #ccc;
}

@media screen and (max-width: 768px) {
  .navbar-menu ul {
    flex-direction: column;
    align-items: center;
  }

  .navbar-menu ul li {
    margin: 10px 0;
  }

  .navbar-menu {
    display: none;
    position: absolute;
    top: 60px;
    left: 0;
    width: 100%;
    background-color: #333;
  }

  .navbar-menu.active {
    display: flex;
  }
}
</style>
