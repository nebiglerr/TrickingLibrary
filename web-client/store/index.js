export const strict = false
const initState = () => ({
})

export const state = initState

export const mutations = {

  reset (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async nuxtServerInit ({ commit, dispatch }, context) {
    await dispatch('tricks/fetchTricks', context)
    await dispatch('submissions/fetchSubmissions', context)
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
