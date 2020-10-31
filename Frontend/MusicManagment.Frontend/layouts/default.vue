<template>
  <div>
    <nav class="navbar" role="navigation" aria-label="main navigation">
      <div class="navbar-brand">
        <NuxtLink :to="localePath('index')" class="navbar-item">
          {{ $t('title') }}
        </NuxtLink>

        <a
          role="button"
          class="navbar-burger burger"
          aria-label="menu"
          aria-expanded="false"
          data-target="navbarBasicExample"
        >
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
          <span aria-hidden="true"></span>
        </a>
      </div>

      <div id="navbarBasicExample" class="navbar-menu">
        <div class="navbar-start">
          <a class="navbar-item"> Home </a>
          <a class="navbar-item"> Documentation </a>
        </div>

        <div class="navbar-end">
          <div class="navbar-item">
            <div v-if="!isAuthenticated">
              <div class="buttons">
                <NuxtLink
                  class="button is-primary"
                  :to="localePath('register')"
                  >{{ $t('navigation.register') }}</NuxtLink
                >
                <NuxtLink
                  class="button is-primary"
                  :to="localePath('login')"
                  >{{ $t('navigation.login') }}</NuxtLink
                >
              </div>
            </div>
            <div v-if="isAuthenticated">
              <div class="buttons">
                <button
                  class="button is-primary"
                  @click="logout()"
                  >{{ $t('navigation.logout') }}</button
                >
              </div>
            </div>
          </div>
        </div>
      </div>
    </nav>
    <div class="container">
      <Nuxt />
    </div>
  </div>
</template>

<script lang='ts'>
import Vue from 'vue';
import { getters } from '~/store/authentication'

export default Vue.extend({
  name: 'Default',

  data() {
    return {
      isAuthenticated: false
    }
  },

  beforeUpdate() {
    this.isAuthenticated = this.$store.getters['authentication/isAuthenticated'] as ReturnType<typeof getters.isAuthenticated>
  },

  methods: {
    logout(): void {
      this.$store.dispatch('authentication/logout');
    }
  }
})
</script>

<style>
html {
  font-family: 'Source Sans Pro', -apple-system, BlinkMacSystemFont, 'Segoe UI',
    Roboto, 'Helvetica Neue', Arial, sans-serif;
  font-size: 16px;
  word-spacing: 1px;
  -ms-text-size-adjust: 100%;
  -webkit-text-size-adjust: 100%;
  -moz-osx-font-smoothing: grayscale;
  -webkit-font-smoothing: antialiased;
  box-sizing: border-box;
}

*,
*::before,
*::after {
  box-sizing: border-box;
  margin: 0;
}

.button--green {
  display: inline-block;
  border-radius: 4px;
  border: 1px solid #3b8070;
  color: #3b8070;
  text-decoration: none;
  padding: 10px 30px;
}

.button--green:hover {
  color: #fff;
  background-color: #3b8070;
}

.button--grey {
  display: inline-block;
  border-radius: 4px;
  border: 1px solid #35495e;
  color: #35495e;
  text-decoration: none;
  padding: 10px 30px;
  margin-left: 15px;
}

.button--grey:hover {
  color: #fff;
  background-color: #35495e;
}
</style>
