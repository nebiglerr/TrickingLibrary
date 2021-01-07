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
  nuxtServerInit ({ dispatch }) {
    return dispatch('tricks/fetchTricks')
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
