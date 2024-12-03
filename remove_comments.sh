for file in $(find . -name "*.cs"); do
sed -i '/^\s*\/\//d' "$file"
done
