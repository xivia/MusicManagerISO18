<template>
  <div>
    <h1 class="title">{{ $t('register.title') }}</h1>
    <form id="register" @submit.prevent="submit">
        <div class="field">
            <label class="label">{{ $t('register.form.userName.name') }}</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.userName.$invalid }" type="text" v-model="userName" :placeholder="$t('register.form.userName.name')">
            </div>
            <p class="help is-danger" v-if="!$v.userName.required">{{ $t('register.form.userName.validation.required') }}</p>
            <p class="help is-danger" v-if="!$v.userName.minLength">{{ $t('register.form.userName.validation.minLength') }}</p>
            <p class="help is-danger" v-if="!$v.userName.alphaNum">{{ $t('register.form.userName.validation.alphaNum') }}</p>
        </div>
        <div class="field">
            <label class="label">{{ $t('register.form.email.name') }}</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.email.$invalid }" type="text" v-model="email" :placeholder="$t('register.form.email.name')">
            </div>
            <!--<p class="help is-danger" v-if="!$v.email.required">{{ $t('register.form.email.validation.required') }}</p>-->
            <p class="help is-danger" v-if="!$v.email.minLength">{{ $t('register.form.email.validation.minLength') }}</p>
            <p class="help is-danger" v-if="!$v.email.email">{{ $t('register.form.email.validation.email') }}</p>
        </div>
        <div class="field">
            <label class="label">{{ $t('register.form.password.name') }}</label>
            <div class="control">
                <input class="input" v-bind:class="{ 'is-danger': $v.password.$invalid }" type="password" v-model="password" :placeholder="$t('register.form.password.name')">
            </div>
            <p class="help is-danger" v-if="!$v.password.required">{{ $t('register.form.password.validation.required') }}</p>
            <p class="help is-danger" v-if="!$v.password.minLength">{{ $t('register.form.password.validation.minLength') }}</p>
        </div>
        <div class="field is-grouped">
            <div class="control">
                <button class="button is-link">{{ $t('register.form.submit') }}</button>
            </div>
            <div class="control">
                <button class="button is-link is-light" type="submit">{{ $t('register.form.cancel') }}</button>
            </div>
        </div>
    </form>
  </div>
</template>

<script lang='ts'>
import Vue from 'vue'
import { required, minLength, maxLength, alphaNum, email } from 'vuelidate/lib/validators'

export default Vue.extend({
  name: 'Register',

  data() {
    return {
      userName: '',
      email: '',
      password: '',
    }
  },

  validations: {
    userName: {
      alphaNum,
      required,
      minLength: minLength(4),
    },
    email: {
      //required,
      email,
      minLength: minLength(4),
    },
    password: {
      required,
      minLength: minLength(8),
      maxLength: maxLength(18)
    },
  },

  methods: {
    submit(): void {
      if (!this.$v.$invalid) {
        this.$store.dispatch('authentication/register', {
          userName: this.$data.userName,
          email: this.$data.email,
          password: this.$data.password
        })
      }
    },
    reset(): void {
      this.$data.userName = '';
      this.$data.email = '';
      this.$data.password = '';
    },
  }
})
</script>
