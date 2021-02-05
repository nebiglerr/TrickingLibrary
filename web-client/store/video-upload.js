export const strict = false
const initState = () => ({
  uploadPromise: null,
  uploadCancelSource: null,
  uploadCompleted: false,
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
  setTask (state, { uploadPromise, source }) {
    console.log(uploadPromise)
    state.uploadPromise = uploadPromise
    state.uploadCancelSource = source
    // state.step++
  },
  completeUpload (state) {
    state.uploadCompleted = true
  },
  resetVideos (state) {
    Object.assign(state, initState())
  }
}

export const actions = {

  startVideoUpload ({ commit, dispatch }, { formData }) {
    const source = this.$axios.CancelToken.source()
    this.$axios.$post('/api/videos', formData, {
      progress: false,
      cancelToken: source.token,
      headers: {
        Accept: 'text/html',
        'Content-Type': 'multipart/form-data'
      }
    }).then((uploadPromise) => {
      commit('completeUpload')
      console.log(source.token)
      console.log(uploadPromise)

      commit('setTask', { uploadPromise, source })
    }).catch((err) => {
      if (this.$axios.isCancel(err)) {

      } else {

      }
    })
  },
  async cancelUpload ({ state, commit }) {
    if (state.uploadPromise) {
      if (state.uploadCompleted) {
        commit('hide')
        console.log(state.uploadPromise)
        const video = await state.uploadPromise
        await this.$axios.$delete('/api/videos/' + video)
      } else {
        state.uploadCancelSource.cancel()
      }
    }
    commit('resetVideos')
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
