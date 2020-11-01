<template>
    <div>
      Start
    </div>
</template>

<script lang="ts">
import Vue from 'vue';

export default Vue.extend({
  name: 'index',
  async beforeMount() {
    const sd = await this.$axios.$post('/api');
    console.log(sd);
  }
});
</script>
