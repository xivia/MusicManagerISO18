<template>
  <div>
    <h1 class="title">{{ $t('login.title') }}</h1>
    <form id="login" @submit.prevent="submit">
        <div class="field">
            <label class="label">{{ $t('login.form.userName.name') }}</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.userName.$invalid }" type="text" v-model="userName" :placeholder="$t('login.form.userName.name')">
            </div>
            <p class="help is-danger" v-if="!$v.userName.required">{{ $t('login.form.userName.validation.required') }}</p>
            <p class="help is-danger" v-if="!$v.userName.minLength">{{ $t('login.form.userName.validation.minLength') }}</p>
            <p class="help is-danger" v-if="!$v.userName.alphaNum">{{ $t('login.form.userName.validation.alphaNum') }}</p>
        </div>
        <div class="field">
            <label class="label">{{ $t('login.form.password.name') }}</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.password.$invalid }" type="text" v-model="password" :placeholder="$t('login.form.password.name')">
            </div>
            <p class="help is-danger" v-if="!$v.password.required">{{ $t('login.form.password.validation.required') }}</p>
            <p class="help is-danger" v-if="!$v.password.minLength">{{ $t('login.form.password.validation.minLength') }}</p>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button class="button is-link">{{ $t('login.form.submit') }}</button>
            </div>
            <div class="control">
                <button class="button is-link is-light" type="submit">{{ $t('login.form.cancel') }}</button>
            </div>
        </div>
    </form>
  </div>
</template>

<script lang='ts'>
import Vue from 'vue'
import { required, minLength, alphaNum } from 'vuelidate/lib/validators'

export default Vue.extend({
  name: 'Login',

  data() {
    return {
      userName: '',
      password: '',
    }
  },

  validations: {
    userName: {
      alphaNum,
      required,
      minLength: minLength(4),
    },
    password: {
      required,
      minLength: minLength(4),
    },
  },
  
  methods: {
    submit(): void {
      if (!this.$v.$invalid) {
        this.$store.dispatch('authentication/login', {
          userName: this.$data.userName,
          password: this.$data.password
        })
      }
    },
    reset(): void {
      this.$data.userName = ''
      this.$data.password = ''
    },
  }
})
</script>