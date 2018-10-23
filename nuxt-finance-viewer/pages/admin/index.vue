<template>
  <div>
    <p>Dashboard for admin user. <a href="..\logout.php">Logout</a></p>
    <form @submit="addItem">
      <input 
        v-model="itemTitle" 
        type="text">
      <input 
        v-model="itemUrl" 
        type="text">
      <button type="submit">Add</button>
    </form>

    <ul>
      <li 
        v-for="n in news" 
        :key="n.title">
        {{ n.title }} - {{ n.url }}
      </li>
    </ul>
    <!-- <bobbles/> -->
  </div>
</template>

<script>
import Bobbles from '~/components/UI/bobbles'

export default {
  components: {
    Bobbles
  },
  data() {
    return {
      news: [
        {
          title: 'Test Title',
          url: '/test-title'
        }
      ]
    }
  },
  methods: {
    addItem: function(e) {
      e.preventDefault()
      this.news.push({ title: this.itemTitle, url: this.itemUrl })
      this.itemTitle = this.itemUrl = ''
    }
  }
}
</script>

<style>
</style>
