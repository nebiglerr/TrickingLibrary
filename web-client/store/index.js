
const initState = () => ({
})

export const state = initState

export const mutations = {

  reset (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async nuxtServerInit ({ commit, dispatch }) {
    await dispatch('tricks/fetchTricks')
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
