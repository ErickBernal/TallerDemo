<template>
  <div class="my-component">
    <h1 v-if="isVisible">{{ message }}</h1>
    <h1 v-bind:class="{ highlighted: isHighlighted }">{{ title }}</h1>
    <button v-on:click="toggleVisibility">Toggle visibility</button>
    <br />
    <button v-on:click="isVisible = !isVisible">Toggle visibility2</button>
    <br />
    <button v-on:click="sayHello">Toggle visibility2</button>
    <br />
    <button v-on:click="toggleHighlight">Toggle visibility4</button>
    <br />
    <button @click="sayHello">Toggle visibility2</button>
    <input v-model="message" />
    <br />
    <ul>
      <li v-for="item in list" :key="item.id">{{ item.text }}</li>
    </ul>
    <br />
    <br />
    <div v-for="post in posts" :key="post.id">
      <h3>{{ post.name }}</h3>
    </div>
  </div>
</template>

<style scoped>
.highlighted {
  color: gold;
}
</style>

<script>
export default {
  name: "MyComponent",
  data() {
    return {
      message: "soy vue",
      isVisible: true,
      title: "Titulo",
      isHighlighted: false,
      list: [
        { id: 1, text: "uno" },
        { id: 2, text: "dos" },
        { id: 3, text: "tres" },
      ],
      posts: [],
    };
  },
  created() {
    fetch("https://localhost:7099/taller/Country")
      .then((response) => response.json())
      .then((data) => (this.posts = data));
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
  },
};
</script>
