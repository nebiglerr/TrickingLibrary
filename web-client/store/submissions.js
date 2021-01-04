export const strict = false
const initState = () => ({
  submissions: []
})

export const state = initState

export const mutations = {
  setSubmissions (state, { submissions }) {
    state.submissions = submissions
  },
  resetTricks (state) {
    Object.assign(state, initState())
  }
}

export const actions = {
  async fetchSubmissionsForTricks ({ commit }, trickId) {
    const submissions = await this.$axios.$get('/api/tricks/' + trickId + '/submissions')
    commit('setSubmissions', { submissions })
    console.log('submissions :', submissions)
  }

  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
