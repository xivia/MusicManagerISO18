import { GetterTree, ActionTree, MutationTree } from 'vuex'

export const state = () => ({
    isAuthenticated: false,
    token: String,
    tokenData: {},
    userName: '',
    email: '',
})

export type RootState = ReturnType<typeof state>

export const getters: GetterTree<RootState, RootState> = {
    isAuthenticated: state => state.isAuthenticated,
    token: state => state.token,
    userName: state => state.userName,
    email: state => state.email
}

export const mutations: MutationTree<RootState> = {
    CHANGE_ISAUTHENTICATED: (state, newIsAuthenticated) => (state.isAuthenticated = newIsAuthenticated),
    CHANGE_TOKEN: (state, newToken) => (state.token = newToken),
    CHANGE_USERNAME: (state, newUserName) => (state.userName = newUserName),
    CHANGE_EMAIL: (state, newEmail) => (state.email = newEmail)
}

export const actions: ActionTree<RootState, RootState> = {
    async register({commit}, data) {
        const response = await this.$axios.$post('/api/user', {
            username: data.userName,
            email: data.email,
            password: data.password
        });

        // if is successful
        if (!response.infos.hasErrors) {
            alert('Success now login');
            commit('CHANGE_USERNAME', data.userName);
            commit('CHANGE_EMAIL', data.email);
            this.$router.push('/login');
        } else {
            console.error(response);
        }
    },
    async login({commit}, data) {
        const response = await this.$axios.$post('/api/user/login', {
            username: data.userName,
            password: data.password
        });

        // if is successful
        if (!response.infos.hasErrors) {
            commit('CHANGE_ISAUTHENTICATED', true);
            commit('CHANGE_TOKEN', response.data.Token);

            this.$axios.interceptors.request.use((config) => {
                // Do something before request is sent
                const isAuthenticated = this.getters['authentication/isAuthenticated'] as ReturnType<typeof getters.isAuthenticated>;
                if (isAuthenticated) {
                  const token = this.getters['authentication/token'] as ReturnType<typeof getters.token>;
                  config.headers['Authorization'] = `Bearer ${token}`;
                  console.log(token);
                }
                return config;
              });
            
            this.$router.push(`/${this.$i18n.locale}`);
        } else {
            console.error(response);
        }
    },
    logout({commit}) {
        commit('CHANGE_ISAUTHENTICATED', false);
        this.$router.push('/login');
    }
}