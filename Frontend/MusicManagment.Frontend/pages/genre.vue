<template>
  <div>
    <div ></div>
    <input class="input" type="text" v-model="newgenre"/>
    <button class="button" v-on:click="create">create</button>
    <ul>
      <span class="tag is-light is-medium" v-for="genre in genreList">{{ genre.name }}</span>
    </ul>
  </div>
</template>
<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  data() {
    return {
      genreList: [],
      newgenre: ''
    }
  },
  async beforeMount() {
    this.$store.dispatch('genre/getGenres');
    const names = this.$store.getters['genre/genres'];
    this.$data.genreList = names.genres;
  },
  methods: {
    create() {
      this.$store.dispatch('genre/createGenre', {
        newGenre: this.$data.newgenre
      });
    }
  }
})
</script>
