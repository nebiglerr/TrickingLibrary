export const strict = false
const initState = () => ({
  submissions: []
})

export const state = initState

export const mutations = {
  setSubmissions (state, submissions) {
    state.submissions = submissions
  },
  resetTricks (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  fetchSubmissions ({ commit }) {
    this.$axios.$get('/api/submissions').then((submissions) => {
      commit('setSubmissions', submissions)
    })
    // console.log('tricks :', tricks)
  }

  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
