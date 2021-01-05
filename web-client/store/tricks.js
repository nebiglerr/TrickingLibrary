export const strict = false
const initState = () => ({
  tricks: [],
  categories: [],
  difficulties: []
})

export const state = initState
export const getters = {
  tricksItem: state => state.tricks.map(x => ({
    text: x.name,
    value: x.id
  })),
  categoriesItem: state => state.categories.map(x => ({
    text: x.name,
    value: x.id
  })),
  difficultiesItem: state => state.difficulties.map(x => ({
    text: x.name,
    value: x.id
  }))
}
export const mutations = {
  setTricks (state, { tricks, categories, difficulties }) {
    state.tricks = tricks
    state.categories = categories
    state.difficulties = difficulties
  },
  resetTricks (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchTricks ({ commit }) {
    const tricks = await this.$axios.$get('/api/tricks')
    const categories = await this.$axios.$get('/api/categories')
    const difficulties = await this.$axios.$get('/api/difficulties')
    commit('setTricks', { tricks, categories, difficulties })
    console.log(tricks, categories, difficulties)
    //
  },

  async createTricks ({ state, commit, dispatch }, form) {
    console.log(form)
    return await this.$axios.$post('/api/tricks', form)
  }

}
