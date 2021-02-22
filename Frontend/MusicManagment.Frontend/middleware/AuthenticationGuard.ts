import { Middleware } from '@nuxt/types'
import { getters } from '~/store/authentication'

const isActive = true

const AuthenticationGuard: Middleware = async ({ store, redirect, route }) => {
  if (isActive) {
    if (!route.path.endsWith('/register') && !route.path.endsWith('/login')) {
      if (
        !store.getters['authentication/isAuthenticated'] as ReturnType<
          typeof getters.isAuthenticated
        >
      ) {
        redirect('/login')
      }
    }
  }
}

export default AuthenticationGuard
