uniform sampler2D texture;

uniform vec2 screenSize;
uniform float scanlineDensity;
uniform float blurDistance;

vec4 blur5(sampler2D image, vec2 uv, vec2 resolution, vec2 direction) {
  vec4 color = vec4(0.0);
  vec2 off1 = vec2(1.3333333333333333) * direction;

  color += texture2D(image, uv) * 0.29411764705882354;
  color += texture2D(image, uv + (off1 / resolution)) * 0.35294117647058826;
  color += texture2D(image, uv - (off1 / resolution)) * 0.35294117647058826;
  return color; 
}

void main()
{
    vec4 finalColor;
    vec2 coords = gl_TexCoord[0].xy;

    finalColor = blur5(texture, coords, screenSize, vec2(blurDistance, 0));
    finalColor += blur5(texture, coords, screenSize, vec2(-blurDistance, 0));

    float actualPixelY = coords.y * screenSize.y;
    float modulus = mod(actualPixelY, scanlineDensity);

    if(int(modulus) == 0 && int(actualPixelY) != 0)
        finalColor /= 1.9;

    finalColor /= 2.15;

    gl_FragColor = finalColor;
}