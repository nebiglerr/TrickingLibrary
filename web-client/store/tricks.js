
const initState = () => ({
  tricks: []
})

export const state = initState

export const mutations = {
  setTricks (state, { tricks }) {
    state.tricks = tricks
  },
  resetTricks (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  fetchTricks ({ commit }) {
    const tricks = this.$axios.$get('http://localhost:5000/api/tricks')
    // console.log('tricks :', tricks)
    commit('setTricks', { tricks })
  },
  createTricks ({ commit, dispatch }, { tricks }) {
    this.$axios.$post('http://localhost:5000/api/tricks', tricks)
    dispatch('fetchTricks')
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
