import { GetterTree, ActionTree, MutationTree } from 'vuex'

export const state = () => ({
    genres: []
})

export type RootState = ReturnType<typeof state>;

export const getters: GetterTree<RootState, RootState> = {
    genres: state => state.genres,
}

export const mutations: MutationTree<RootState> = {
    CHANGE_GENRES: (state, newGenres) => (state.genres = newGenres),
}

export const actions: ActionTree<RootState, RootState> = {
    async getGenres({commit}) {
        const response = await this.$axios.$get('/api/genre/')

        if (!response.infos.hasErrors) {
            commit('CHANGE_GENRES', response.data);
        } else {
            console.error(response);
        }

    },
    async createGenre({commit, actions}, data) {
        const response = await this.$axios.$post('/api/genre', {
            genreName: data.newGenre
        });
        console.log(response);
    }
}
