
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
    const uploadPromised = this.$axios.post('http://localhost:5000/api/videos', formData, {
      headers: {
        Accept: 'text/html',
        'Content-Type': 'multipart/form-data'
      }
    })
    commit('setTask', { uploadPromised })
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
