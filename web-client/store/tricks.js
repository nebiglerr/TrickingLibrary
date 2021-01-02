export const strict = false
const initState = () => ({
  tricks: []
})

export const state = initState

export const mutations = {
  setTricks (state, tricks) {
    state.tricks = tricks
  },
  resetTricks (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  fetchTricks ({ commit }) {
    this.$axios.$get('/api/tricks').then((tricks) => {
      commit('setTricks', tricks)
    })
    // console.log('tricks :', tricks)
  }

  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
