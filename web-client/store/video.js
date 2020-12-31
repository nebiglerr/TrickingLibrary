export const strict = false
const initState = () => ({
  uploadPromise: null
})

export const state = initState

export const mutations = {
  setTask (state, { uploadPromise }) {
    state.uploadPromise = uploadPromise
  },
  resetVideos (state) {
    Object.assign(state, initState())
  }
}

export const actions = {

  startVideoUpload ({ commit, dispatch }, { formData }) {
    const uploadPromise = this.$axios.$post('http://localhost:5000/api/videos', { formData }, { isJSON: true })
    commit('setTask', { uploadPromise })
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
