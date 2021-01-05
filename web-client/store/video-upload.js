export const strict = false
const initState = () => ({
  uploadPromise: null,
  active: false,
  component: null
})

export const state = initState

export const mutations = {
  activate (state, { component }) {
    state.active = true
    state.component = component
  },
  hide (state) {
    state.active = false
  },
  setTask (state, uploadPromise) {
    state.uploadPromise = uploadPromise
    state.step++
  },
  resetVideos (state) {
    Object.assign(state, initState())
  }
}

export const actions = {

  startVideoUpload ({ commit, dispatch }, formData) {
    this.$axios.$post('/api/videos', formData, {
      headers: {
        Accept: 'text/html',
        'Content-Type': 'multipart/form-data'
      }
    }).then((element) => {
      console.log(element)

      commit('setTask', element)
    })
  },
  async createSubmission ({ state, commit, dispatch }, { form }) {
    if (!state.uploadPromise) {
      return
    }

    form.video = await state.uploadPromise

    await dispatch('submissions/createSubmission', { form }, { root: true })
  //  commit('resetVideos')
  }

  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
