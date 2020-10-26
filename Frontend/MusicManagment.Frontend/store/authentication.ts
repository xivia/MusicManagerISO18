import { GetterTree, ActionTree, MutationTree } from 'vuex'

export const state = () => ({
    isAuthenticated: false,
    error: {},
    userName: '',
    email: '',
    from: {
        register: {
            userName: '',
            email: '',
            password: ''
        }
    }
})

export type RootState = ReturnType<typeof state>

export const getters: GetterTree<RootState, RootState> = {
    isAuthenticated: state => state.isAuthenticated,
    userName: state => state.userName,
    email: state => state.email,
}

export const mutations: MutationTree<RootState> = {
    CHANGE_ISAUTHENTICATED: (state, newIsAuthenticated) => (state.isAuthenticated = newIsAuthenticated),
    CHANGE_ERROR: (state, newError) => (state.error = newError),
    CHANGE_USERNAME: (state, newUserName) => (state.userName = newUserName),
    CHANGE_EMAIL: (state, newEmail) => (state.email = newEmail),
    RESET_REGISTER_FROM: (state, ignore) => {
        state.from.register = {
            userName: '',
            email: '',
            password: ''
        }
    }
}

export const actions: ActionTree<RootState, RootState> = {
    async register({commit}) {
        const response = await this.$axios.$put('/api/authenticate', {
            username: this.state.from.register.userName,
            email: this.state.from.register.email,
            password: this.state.from.register.password
        });

        // if is successful
        if (true) {
            commit('CHANGE_ISAUTHENTICATED', true)
            commit('CHANGE_USERNAME', this.state.from.register.userName);
            commit('CHANGE_EMAIL', this.state.from.register.password);
        
            commit('RESET_REGISTER_FROM', true);
        } else {
            commit('CHANGE_ERORR', response);
        }
    }
}