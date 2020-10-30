import { GetterTree, ActionTree, MutationTree } from 'vuex'

export const state = () => ({
    isAuthenticated: false,
    error: {},
    userName: '',
})

export type RootState = ReturnType<typeof state>

export const getters: GetterTree<RootState, RootState> = {
    isAuthenticated: state => state.isAuthenticated,
    userName: state => state.userName
}

export const mutations: MutationTree<RootState> = {
    CHANGE_ISAUTHENTICATED: (state, newIsAuthenticated) => (state.isAuthenticated = newIsAuthenticated),
    CHANGE_ERROR: (state, newError) => (state.error = newError),
    CHANGE_USERNAME: (state, newUserName) => (state.userName = newUserName),
}

export const actions: ActionTree<RootState, RootState> = {
    async register({commit}, data) {
        const response = await this.$axios.$post('/api/authenticate', {
            username: data.userName,
            password: data.password
        });

        // if is successful
        if (true) {
            commit('CHANGE_ISAUTHENTICATED', true)
            commit('CHANGE_USERNAME', data.userName);
        
            commit('RESET_REGISTER_FROM', true);
        } else {
            commit('CHANGE_ERORR', response);
        }
    }
}