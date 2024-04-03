<template>
  <nav class="navbar">
    <div class="navbar-brand">
      <div>
        <router-link to="/">Inicio</router-link>
      </div>
      <div>
        <router-link to="/servicios">Servicios</router-link>
      </div>
    </div>

    <div class="navbar-menu" :class="{ active: isMenuActive }">
      <div class="menu-toggle" @click="toggleMenu">Menú</div>
      <ul v-show="isMenuActive">
        <li>
          <button @click="verificarContrasena">Logs</button>
        </li>
      </ul>
    </div>
  </nav>
</template>

<script>
export default {
  data() {
    return {
      isMenuActive: false,
    };
  },
  methods: {
    toggleMenu() {
      this.isMenuActive = !this.isMenuActive;
    },
    async verificarContrasena() {
      const contrasena = prompt("Ingrese la contraseña:");
      if (!contrasena) return; // Si no se ingresa contraseña, salir

      try {
        const response = await fetch("https://localhost:7099/taller/Owner/1");
        const ownerData = await response.json();
        if (ownerData.pwd === contrasena) {
          this.$router.push("/Logs");
        } else {
          alert("Contraseña incorrecta. Acceso denegado.");
          this.$router.push("/");
        }
      } catch (error) {
        console.error("Error al verificar la contraseña:", error);
        alert(
          "Error al verificar la contraseña. Por favor, intenta de nuevo más tarde."
        );
      }
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
  display: flex; /* Alinea los elementos horizontalmente */
  justify-content: center; /* Centra los elementos horizontalmente */
}

.navbar-brand div {
  margin-right: 20px; /* Espacio entre los elementos */
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
