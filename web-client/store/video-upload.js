
export const strict = false
const initState = () => ({
  uploadPromise: null,
  active: false,
  type: '',
  step: 1
})

export const state = initState

export const mutations = {
  toggleActivity (state) {
    state.active = !state.active
    if (!state.active) {
      Object.assign(state, initState())
    }
  },
  setTask (state, uploadPromise) {
    state.uploadPromise = uploadPromise
    state.step++
  },
  setType (state, { type }) {
    state.type = type
    state.step++
  },
  resetVideos (state) {
    Object.assign(state, initState())
  }
}

export const actions = {

  startVideoUpload ({ commit, dispatch }, { formData }) {
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
  async createTricks ({ context, commit, dispatch }, { tricks }) {
    await this.$axios.post('/api/tricks', tricks)
    await dispatch('tricks/fetchTricks', null, { root: true })
  }
  /* async nuxtServerInit({dispatch}) {
    await dispatch("auth/initialize")
    await dispatch("library/loadContent")
  } */
}
