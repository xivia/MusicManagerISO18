import { GetterTree, ActionTree, MutationTree } from 'vuex'

export const state = () => ({
    isAuthenticated: false,
    error: {},
    userName: '',
    email: '',
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
}

export const actions: ActionTree<RootState, RootState> = {
    async register({commit}, data) {
        console.log('yes');
        const response = await this.$axios.$post('/api/authenticate', {
            username: data.userName,
            email: data.email,
            password: data.password
        });

        // if is successful
        if (true) {
            commit('CHANGE_ISAUTHENTICATED', true)
            commit('CHANGE_USERNAME', data.userName);
            commit('CHANGE_EMAIL', data.email);
        
            commit('RESET_REGISTER_FROM', true);
        } else {
            commit('CHANGE_ERORR', response);
        }
    }
}