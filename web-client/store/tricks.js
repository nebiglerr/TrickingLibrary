export const strict = false
const initState = () => ({
  tricks: []
})

export const state = initState

export const mutations = {
  setTricks (state, tricks) {
    console.log(tricks)
    state.tricks = tricks
  },
  resetTricks (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchTricks ({ commit }) {
    const tricks = await this.$axios.$get('/api/tricks')
    commit('setTricks', tricks)
    console.log(tricks)
    //
  }

  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
