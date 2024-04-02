<template>
  <div class="mint-container">
    <div class="Wallcome">
      <h1>{{ title }}</h1>
      <div class="button-container">
        <button
          @mouseover="toggleHover(1)"
          @mouseleave="toggleHover(1)"
          :class="{ highlighted: isHovering[1] }"
          v-on:click="toggleVisibility"
        >
          Repuestos
        </button>
        <button
          @mouseover="toggleHover(2)"
          @mouseleave="toggleHover(2)"
          :class="{ highlighted: isHovering[2] }"
          @click="sayHello"
        >
          Servicio
        </button>
      </div>
      <br />
    </div>
  </div>
</template>

<style scoped>
.mint-container {
  background-color: #c7fbc7; /* Color verde menta */
  padding: 20px; /* Añadir espacio alrededor del contenido */
  width: 60%; /* Ancho del contenedor del 60% de la pantalla */
  margin: 0 auto; /* Centrar el contenedor horizontalmente */
}

@media (max-width: 768px) {
  .mint-container {
    width: 90%; /* Reducir el ancho del contenedor en pantallas más pequeñas */
  }
}

.button-container {
  display: flex;
}

button {
  width: 150px; /* Ancho del botón */
  height: 150px; /* Altura del botón */
  margin-right: 20px; /* Margen derecho entre los botones */
  border-radius: 10px; /* Radio del borde para hacerlo cuadrado */
  font-size: 16px; /* Tamaño del texto */
}

.button-container button:last-child {
  margin-right: 0; /* Eliminar margen derecho del último botón */
}

.highlighted {
  color: gold;
  cursor: pointer; /* Cambia el cursor al pasar sobre el botón */
}
</style>

<script>
export default {
  name: "Wallcome",
  data() {
    return {
      title: "Taller",
      isHovering: { 1: false, 2: false }, // Objeto para rastrear si el mouse está sobre cada botón
    };
  },

  methods: {
    toggleVisibility() {
      // Implementación de la función de visibilidad (no se especificó en la solicitud)
    },
    sayHello() {
      console.log("Hello!"); // Solo para fines de demostración
    },
    toggleHover(buttonNumber) {
      this.isHovering[buttonNumber] = !this.isHovering[buttonNumber];
    },
  },
};
</script>
